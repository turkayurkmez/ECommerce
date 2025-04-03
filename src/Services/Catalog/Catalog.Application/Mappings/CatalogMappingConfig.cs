using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Mappings
{
    public class CatalogMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Product -> ProductDto

            config.NewConfig<Product, ProductDto>()
                  .Map(dest => dest.Status, src => src.Status.ToString())
                  .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty)
                  .Map(dest => dest.BrandName, src => src.Brand != null ? src.Brand.Name : string.Empty)
                  .Map(dest => dest.Images, src => src.ProductImages.Adapt<List<ProductImageDto>>())
                  .Map(dest => dest.Attributes, src => src.ProductAttributes.Adapt<List<ProductAttributeDto>>());

            //Product -> ProductSummaryDto

            config.NewConfig<Product, ProductSummaryDto>()
                  .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty)
                  .Map(dest => dest.BrandName, src => src.Brand != null ? src.Brand.Name : string.Empty)
                  .Map(dest => dest.Status, src => src.Status.ToString())
                  .Map(dest => dest.MainImageUrl, src => GetMainImageUrl(src.ProductImages));

            //Create ProductDto -> Product (For Command Handlers)

            config.NewConfig<CreateProductDto, Product>()
                 .MapToConstructor(true)
                 .ConstructUsing((CreateProductDto src) => new Product(src.Name, src.Description, src.Price, src.StockQuantity, src.SKU, src.CategoryId, src.BrandId));

            // Category -> CategoryDto (gerekirse)


            // Brand -> BrandDto (gerekirse)





        }

        private string GetMainImageUrl(IReadOnlyCollection<ProductImage> productImages)
        {
            var mainImage = productImages.FirstOrDefault(img => img.IsMain);
            if (mainImage != null)
            {
                return mainImage.ImageUrl;
            }

            var firstImage = productImages.FirstOrDefault();
            return firstImage != null ? firstImage.ImageUrl : string.Empty;
        }
    }
}
