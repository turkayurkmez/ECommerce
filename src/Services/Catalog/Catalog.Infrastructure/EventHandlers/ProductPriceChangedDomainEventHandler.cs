using ECommerce.Catalog.Domain.Events;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.EventBus.Events.Catalog;
using ECommerce.MessageBroker.Interfaces;
using MediatR;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventHandlers;

//Product price changed domain event handler
public class ProductPriceChangedDomainEventHandler : INotificationHandler<ProductPriceChangedDomainEvent>
{
    private readonly ILogger<ProductPriceChangedDomainEventHandler> _logger;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IProductRepository _productRepository;




    public ProductPriceChangedDomainEventHandler(ILogger<ProductPriceChangedDomainEventHandler> logger,  IMessagePublisher messagePublisher, IProductRepository productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      
        _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Ürün fiyatı değişti: {notification.ProductId}, {notification.OldPrice}, {notification.NewPrice}");
        //Burada daha sonra masstransit gibi bir sistemle başka bir servise mesaj göndereceğiz.
        try
        {
            //ürün bilgisini getir:
            var product = await _productRepository.GetByIdAsync(notification.ProductId, cancellationToken);
            if (product == null)
            {
                _logger.LogWarning($"Ürün bulunamadı: {notification.ProductId}");
                return;
            }
            ////ürün kategorisini getir:
            //string categoryName = string.Empty;
            //if (product.CategoryId.HasValue)
            //{
            //    var category = await _categoryRepository.GetByIdAsync(product.CategoryId.Value, cancellationToken);
            //    categoryName = category?.Name ?? string.Empty;

            //}

            //integration event oluştur:
            var productPriceChangedEvent = new ProductPriceChangedEvent
            {
                ProductId = product.Id,
                OldPrice = notification.OldPrice,
                Price = notification.NewPrice           

            };

            //mesajı gönder:
            await _messagePublisher.PublishAsync(productPriceChangedEvent, cancellationToken);
            _logger.LogInformation($"Ürün fiyatı değişiklik mesajı gönderildi: {productPriceChangedEvent.ProductId}, {productPriceChangedEvent.OldPrice}, {productPriceChangedEvent.Price}");

        }
        catch (Exception)
        {
            _logger.LogError($"Ürün fiyatı değişiklik mesajı gönderilirken hata oluştu: {notification.ProductId}");
            throw;
        }
        finally
        {
            //işlem tamamlandı:
            _logger.LogInformation($"Ürün fiyatı değişiklik işlemi tamamlandı: {notification.ProductId}");


        }
    }
}





