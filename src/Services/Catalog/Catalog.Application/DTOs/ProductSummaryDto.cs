namespace ECommerce.Catalog.Application.DTOs
{
    public record ProductSummaryDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public string Status { get; init; } = string.Empty;
        public string CategoryName { get; init; } = string.Empty;
        public string BrandName { get; init; } = string.Empty;
        public string MainImageUrl { get; init; } = string.Empty;
    }





}