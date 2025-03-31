namespace ECommerce.EventBus.Events.Basket
{
    public record BasketCheckoutItemEvent
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Quantity { get; init; }

        //Total Price = Price * Quantity
        public decimal TotalPrice { get; init; }
    }
}
