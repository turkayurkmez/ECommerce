namespace ECommerce.Catalog.Application.DTOs
{
    //Ürün resmi için kullanılacak olan DTO (ImageUrl, Id, IsMain, SortOrder):

    public record ProductImageDto
    {
        public string ImageUrl { get; init; } = string.Empty;
        public int Id { get; init; }
        public bool IsMain { get; init; }
        public int SortOrder { get; init ; }
    }





}