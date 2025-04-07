using ECommerce.Basket.Domain.Entities;
using ECommerce.Basket.Domain.Repositories;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

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
            var shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(basket!);
            return shoppingCart!;

        }
        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(shoppingCart, nameof(shoppingCart));
            try
            {
                var created = await _database.StringSetAsync(shoppingCart.UserId, JsonSerializer.Serialize(shoppingCart), TimeSpan.FromDays(30));

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
    }

}
