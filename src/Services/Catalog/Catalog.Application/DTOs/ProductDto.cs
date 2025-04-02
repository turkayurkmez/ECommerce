using System;

namespace Catalog.Application.DTOs
{

    //Görüntüleme için kullanılacak olan record DTO:

    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public string SKU { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
        public int BrandId { get; init; }
        public string BrandName { get; init; } = string.Empty;
        public IEnumerable<ProductImageDto> Images { get; init; } = Enumerable.Empty<ProductImageDto>();
        public IEnumerable<ProductAttributeDto> Attributes { get; init; } = Enumerable.Empty<ProductAttributeDto>();
        public DateTime CreatedDate { get; init; }
        public DateTime LastModifiedDate { get; init; }
    }





}