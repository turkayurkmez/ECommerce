using ECommerce.Basket.Domain.Repositories;
using ECommerce.Basket.Infrastructure.Repositories;
using ECommerce.EventBus.Events.Catalog;
using ECommerce.MessageBroker.Implementations;
using StackExchange.Redis;

namespace ECommerce.Basket.API.Consumers
{
    public class ProductPriceChangedEventConsumer : BaseConsumer<ProductPriceChangedEvent>
    {
        private readonly ILogger<ProductPriceChangedEventConsumer> _logger;
        private readonly IBasketRepository _basketRepository;

        public ProductPriceChangedEventConsumer(ILogger<ProductPriceChangedEventConsumer> logger, IBasketRepository basketRepository):base(logger)
        {
            _logger = logger;
            _basketRepository = basketRepository;

        }
        protected override async Task ProcessMessageAsync(ProductPriceChangedEvent message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Ürün fiyatı değişti! Ürün Id: {message.ProductId} eski fiyat: {message.OldPrice} yeni fiyat: {message.Price}");

            // Tüm sepetleri güncelleme işlemi normalde oldukça kaynak yoğun olabilir
            // Gerçek uygulamada, sepet-ürün ilişkisi için bir index veya cache yapısı kullanılabilir
            // Bu örnekte basitlik için Redis'in tüm keylerini alıp sepetleri tek tek kontrol edeceğiz

            // Redis üzerindeki tüm kullanıcı ID'lerini al
            // NOT: Gerçek uygulamada bu şekilde yapılmaz, sadece örnek amaçlıdır
            var userIds = await GetAllUserIdAsync();

            //her kullanıcının sepetini kontrol et: 
            foreach (var userId in userIds) 
            {
                var basket = await _basketRepository.GetBasketAsync(userId, cancellationToken);
                if (basket == null) continue;
                // Sepetteki ürünleri kontrol et
                
                basket.UpdateItemPrice(message.ProductId, message.Price);
                //if (item != null)
                //{
                //    // Ürün fiyatını güncelle
                //    item.ChangePrice(message.Price);
                //    await _basketRepository.UpdateBasketAsync(basket, cancellationToken);
                //    _logger.LogInformation($"Sepet güncellendi! Kullanıcı Id: {userId} Ürün Id: {message.ProductId} yeni fiyat: {message.Price}");
                //}

                // Sepeti güncelle
                await _basketRepository.UpdateBasketAsync(basket, cancellationToken);
                _logger.LogInformation($"Sepet güncellendi! Kullanıcı Id: {userId} Ürün Id: {message.ProductId} yeni fiyat: {message.Price}");

            }



        }

        private async Task<string[]> GetAllUserIdAsync()
        {
            return await _basketRepository.GetAllUserIdsAsync();
        }
    }

}
