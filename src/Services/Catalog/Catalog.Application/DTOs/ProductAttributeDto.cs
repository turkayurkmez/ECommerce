namespace ECommerce.Catalog.Application.DTOs
{
    //Ürün özellikleri için kullanılacak olan DTO (Key, Value)

    public record ProductAttributeDto
    {
        public string Key { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;
    }





}