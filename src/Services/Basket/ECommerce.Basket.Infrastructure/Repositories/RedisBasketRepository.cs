using ECommerce.Basket.Domain.Entities;
using ECommerce.Basket.Domain.Repositories;
using ECommerce.Basket.Infrastructure.Resolvers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace ECommerce.Basket.Infrastructure.Repositories
{
    public class RedisBasketRepository : IBasketRepository
    {


        //Redis bağlantısı için gerekli olan kodlar burada olacak.
        //Örnek olarak StackExchange.Redis kütüphanesini kullanabilirsiniz.
        private readonly IConnectionMultiplexer _redis;

        private readonly IDatabase _database;
        private readonly ILogger<RedisBasketRepository> _logger;
        public RedisBasketRepository(IConnectionMultiplexer redis, ILogger<RedisBasketRepository> logger)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
            _logger = logger;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            var basket = await _database.StringGetAsync(userId);
            if (basket.IsNullOrEmpty)
            {
                _logger.LogWarning($"{userId} kullanıcısına ait sepet bilgisi bulunamadı");
                return null;
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor 
            };

            var basketDto = JsonConvert.DeserializeObject<ShoppingCartDto>(basket!);

            // DTO'dan domain modele dönüştür
            var shoppingCart = new ShoppingCart(basketDto.UserId, basketDto.UserName);

            foreach (var itemDto in basketDto.Items)
            {
                shoppingCart.AddItem(itemDto.ProductId, itemDto.ProductName, itemDto.PictureUrl, (double)itemDto.UnitPrice, itemDto.Quantity);
                
            }

            return shoppingCart!;

        }
        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(shoppingCart, nameof(shoppingCart));
            try
            {
                var basketDto = new ShoppingCartDto
                {
                    UserId = shoppingCart.UserId,
                    UserName = shoppingCart.UserName,
                    Items = shoppingCart.Items.Select(i => new ShoppingCartItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        UnitPrice = i.Price,
                        Quantity = i.Quantity,
                        PictureUrl = i.ProductImageUrl
                    }).ToList()
                };
                var created = await _database.StringSetAsync(shoppingCart.UserId, JsonConvert.SerializeObject(basketDto), TimeSpan.FromDays(30));

                if (!created)
                {
                    _logger.LogError($"{shoppingCart.UserId} kullanıcısına ait sepet bilgisi güncellenemedi");
                    return null;
                }

                _logger.LogInformation($"{shoppingCart.UserId} kullanıcısına ait sepet bilgisi güncellendi");
                return await GetBasketAsync(shoppingCart.UserId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{shoppingCart.UserId} kullanıcısına ait sepet bilgisi güncellenirken hata oluştu");

                return null;
            }

        }
        public async Task DeleteBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleted = await _database.KeyDeleteAsync(userId);
                if (!deleted)
                {
                    _logger.LogWarning($"{userId} kullanıcısına ait sepet bilgisi silinemedi");
                }
                else
                {
                    _logger.LogInformation($"{userId} kullanıcısına ait sepet bilgisi silindi");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{userId} kullanıcısına ait sepet bilgisi silinirken hata oluştu");
            }


        }

        public async Task<string[]> GetAllUserIdsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var server = _redis.GetServer(_redis.GetEndPoints().First());
                var keys = server.Keys(pattern: "*").ToArray();
                var userIds = keys.Select(k => k.ToString()).ToArray();
                return userIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ID'leri alınırken hata oluştu");

                return Array.Empty<string>();
            }

        }
    }

    public class ShoppingCartDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new();
    }

    public class ShoppingCartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }


}
