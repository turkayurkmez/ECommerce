using Catalog.Domain.Events;
using Catalog.Domain.ValueObjects;
using ECommerce.Common.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{
    public class Product : AuditableEntity<int>, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        //SKU: Stock Keeping Unit
        public string SKU { get; private set; } = string.Empty;

        public ProductStatus Status { get; private set; }

        //CategoryID
        public int CategoryId { get; private set; }
        //BrandID
        public int BrandId { get; private set; }

        //Navigation properties
        public Category Category { get; private set; }
        public Brand Brand { get; private set; }

        private readonly List<ProductImage> _productImages = new List<ProductImage>();
        public IReadOnlyCollection<ProductImage> ProductImages => _productImages.AsReadOnly();

        private readonly List<ProductAttribute> _productAttributes = new List<ProductAttribute>();
        public IReadOnlyCollection<ProductAttribute> ProductAttributes => _productAttributes.AsReadOnly();

       

        protected Product()
        {
        }

        public Product(string name, string description, decimal price, int stockQuantity, string sku, int categoryId, int brandId)
        {
            //check name:
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Ürün adı boş olamaz:");
            }
            //check description:
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), "Ürün açıklaması boş olamaz:");
            }
            //check price:
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Ürün fiyatı 0'dan büyük olmalıdır:");
            }
            //check stockQuantity:
            if (stockQuantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stok miktarı 0'dan küçük olamaz:");
            }
            //check sku:
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku), "Ürün SKU değeri boş olamaz:");
            }
            //check categoryId:
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Kategori ID değeri geçersiz:");
            }
            //check brandId:
            if (brandId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(brandId), "Marka ID değeri geçersiz:");
            }
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            SKU = sku;
            Status = StockQuantity > 0 ? ProductStatus.Active : ProductStatus.OutOfStock ;
            CategoryId = categoryId;
            BrandId = brandId;

            AddDomainEvent(new ProductCreatedDomainEvent(Id, Name, Price, StockQuantity));

        }

        //Update Basic Info

        public void UpdateBasicInfo(string name, string description, string sku)
        {
            //check name:
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Ürün adı boş olamaz:");
            }
            //check description:
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), "Ürün açıklaması boş olamaz:");
            }      
       
            //check sku:
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku), "Ürün SKU değeri boş olamaz:");
            }
            Name = name;
            Description = description;         
            SKU = sku;
            AddDomainEvent(new ProductUpdatedDomainEvent(Id, Name));
            SetModifiedDate();
        }

        //Change Price

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice), "Ürün fiyatı 0'dan büyük olmalıdır:");
            }

            //if new price is equal to old price, do not raise domain event
            if (Price == newPrice)
            {
                return;
            }

            var oldPrice = Price;
            Price = newPrice;
            AddDomainEvent(new ProductPriceChangedDomainEvent(Id, oldPrice, Price));
            SetModifiedDate();
        }

        //Update Stock Quantity

        public void ChangeStock(int newStock)
        {
            if (newStock <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newStock), "Stok miktarı 0'dan büyük olmalıdır:");

            }

            //if new stock is equal to old stock, do not raise domain event
            if (StockQuantity == newStock)
            {
                return;
            }

            Status = StockQuantity > 0 ? ProductStatus.Active : ProductStatus.OutOfStock;

            var oldStock = StockQuantity;
            StockQuantity = newStock;
            AddDomainEvent(new ProductStockQuantityUpdatedDomainEvent(Id, oldStock, StockQuantity));
            SetModifiedDate();
        }

        //Change Category:

        public void ChangeCategory(int newCategoryId)
        {
            if (newCategoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newCategoryId), "Kategori ID değeri geçersiz:");
            }
            if (CategoryId == newCategoryId)
            {
                return;
            }
            CategoryId = newCategoryId;
            SetModifiedDate();
        }

        //Change Brand:

        public void ChangeBrand(int newBrandId)
        {
            if (newBrandId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newBrandId), "Marka ID değeri geçersiz:");
            }
            if (BrandId == newBrandId)
            {
                return;
            }
            BrandId = newBrandId;
            SetModifiedDate();
        }

        //Add Product Image

        public void AddProductImage(string imageUrl, bool isMain=false, int sortOrder=0)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentNullException(nameof(imageUrl), "Resim URL değeri boş olamaz:");
            }

            //if isMain is true, set other images as non-main
            if (isMain)
            {
                foreach (var image in _productImages)
                {
                    image.SetAsNonMainImage();
                }
            }
            var productImage = new ProductImage(Id, imageUrl, isMain, sortOrder);
            _productImages.Add(productImage);
            SetModifiedDate();
        }

        //Remove Product Image

        public void RemoveProductImage(int productImageId)
        {
            var productImage = _productImages.FirstOrDefault(x => x.Id == productImageId);
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImageId), "Ürün resmi bulunamadı:");
            }
            _productImages.Remove(productImage);
            SetModifiedDate();
        }

        //Add Product Attribute

        public void AddAttribute(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "Ürün özelliği anahtarı boş olamaz:");
            }

            var existingAttribute = _productAttributes.FirstOrDefault(x => x.Key == key);
            if (existingAttribute != null)
            {
                _productAttributes.Remove(existingAttribute);
            }
            _productAttributes.Add(new ProductAttribute(key, value));
            SetModifiedDate();



        }

        //Remove Product Attribute
        public void RemoveAttribute(string key) {
            var existingAttribute = _productAttributes.FirstOrDefault(x => x.Key == key);
            if (existingAttribute == null)
            {
                throw new ArgumentNullException(nameof(key), "Ürün özelliği bulunamadı:");
            }
            _productAttributes.Remove(existingAttribute);
            SetModifiedDate();
        }

        //Delete Product

        public void Delete()
        {
            AddDomainEvent(new ProductDeletedDomainEvent(Id));
        }





    }
}
