namespace ECommerce.EventBus.Events.Basket
{
    public record BasketCheckoutEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string UserName { get; init; } = string.Empty;
        public decimal TotalPrice { get; init; }

        //Billing Address
        public string BillingAddress { get; init; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public List<BasketCheckoutItemEvent> Items { get; init; } = new();
    }
}
