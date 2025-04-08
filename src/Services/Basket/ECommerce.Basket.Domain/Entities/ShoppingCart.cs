using ECommerce.Common.Domain;

namespace ECommerce.Basket.Domain.Entities
{
    public class ShoppingCart : Entity<string>, IAggregateRoot
    {
        public string UserId { get; private set; }
        public string? UserName { get; private set; }
        private List<ShoppingCartItem> _items = new();
        public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();
        //public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

        //ef core için boş constructor
        public ShoppingCart() { }
        public ShoppingCart(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;

        }

        //Add item to cart
        public void AddItem(int productId, string productName, string productImageUrl, double price, int quantity)
        {
            var existingItem = _items.Find(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {
                var newItem = new ShoppingCartItem(productId, productName, productImageUrl, (decimal)price, quantity);
                _items.Add(newItem);
            }
        }

        //Update item:
        public void UpdateItem(int productId, int quantity)
        {
            var existingItem = _items.Find(i => i.ProductId == productId);
            if (existingItem != null)
            {
                //quantity 0'dan küçükse sil:
                if (quantity <= 0)
                {
                    _items.Remove(existingItem);
                }
                else
                {
                    existingItem.UpdateQuantity(quantity);
                }
            }

        }

        //Remove item:
        public void RemoveItem(int productId)
        {
            var existingItem = _items.Find(i => i.ProductId == productId);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
            }
        }

        public void UpdateItemPrice(int productId, decimal price)
        {
            var existingItem = _items.Find(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.ChangePrice(price);
            }
        }

        public void Clear() => _items.Clear();

        //Get total price
        public decimal GetTotalPrice()
        {
            return _items.Sum(i => i.GetTotalPrice());
        }






    }
}

