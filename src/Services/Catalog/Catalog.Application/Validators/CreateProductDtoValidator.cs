using Catalog.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş olamaz")
                                .MaximumLength(100)
                                .WithMessage("Ürün adı en fazla 100 karakter olabilir");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün açıklaması boş olamaz").
                                        MaximumLength(1000).WithMessage("Ürün açıklaması en fazla 1000 karakter olabilir");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage("Ürün stok adedi 0'dan büyük olmalıdır");
            RuleFor(x => x.SKU).NotEmpty().WithMessage("Ürün SKU boş olamaz")
                               .MaximumLength(50).WithMessage("Ürün SKU en fazla 50 karakter olabilir");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Kategori id 0'dan büyük olmalıdır");
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("Marka id 0'dan büyük olmalıdır");
        }

    }

    //UpdateProductDtoValidator

    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto> {

        public UpdateProductDtoValidator() {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş olamaz")
                                .MaximumLength(100)
                                .WithMessage("Ürün adı en fazla 100 karakter olabilir");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün açıklaması boş olamaz").
                                        MaximumLength(1000).WithMessage("Ürün açıklaması en fazla 1000 karakter olabilir");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage("Ürün stok adedi 0'dan büyük olmalıdır");
            RuleFor(x => x.SKU).NotEmpty().WithMessage("Ürün SKU boş olamaz")
                               .MaximumLength(50).WithMessage("Ürün SKU en fazla 50 karakter olabilir");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Kategori id 0'dan büyük olmalıdır");
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("Marka id 0'dan büyük olmalıdır");

        }
    }


}
