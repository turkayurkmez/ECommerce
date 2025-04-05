namespace Catalog.Application.DTOs
{
    public record CategoryDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;        
        public bool IsActive { get; init; }
        public int? ParentCategoryId { get; init; }
        public int Level { get; init; } = 1;
    }
   
}