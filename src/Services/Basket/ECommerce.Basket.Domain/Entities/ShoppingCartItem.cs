using ECommerce.Common.Domain;

namespace ECommerce.Basket.Domain.Entities
{
    public class ShoppingCartItem : Entity<int>
    {

        //ProductId ddd:
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImageUrl { get; private set; }


        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public ShoppingCartItem(int productId, string productName, string productImageUrl, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductImageUrl = productImageUrl;
            Price = price;
            Quantity = quantity > 0 ? quantity:1;

        }
        //ef core için boş constructor
        public ShoppingCartItem() { }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }
            Quantity = quantity;
            SetModifiedDate();
        }

        //Get total price
        public decimal GetTotalPrice()
        {
            return Price * Quantity;
        }


    }
}