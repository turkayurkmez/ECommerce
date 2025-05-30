﻿namespace ECommerce.Catalog.Application.DTOs
{
    // Ürün güncelleme komutu için DTO
    public record UpdateProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public string SKU { get; init; } = string.Empty;
        public int CategoryId { get; init; }
        public int BrandId { get; init; }
    }





}