namespace ECommerce.Basket.Domain.Events
{
    public record BasketCheckoutItemEvent
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
      
        public decimal Price { get; init; }
        public int Quantity { get; init; }

        //Total price:
        public decimal TotalPrice => Price * Quantity;
    }
}
