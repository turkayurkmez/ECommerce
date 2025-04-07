using ECommerce.Basket.Domain.Events;
using ECommerce.Basket.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Features.Commands
{
    public record CheckoutBasketCommand : IRequest<Result>
    {
        public string UserId { get; init; }
        public string UserName { get; init; }

        // Fatura Bilgileri
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string EmailAddress { get; init; }
        public string BillingAddress { get; init; }
        public string ShippingAddress { get; init; }

        // Ödeme Bilgileri
        public string PaymentMethod { get; init; }
    }
    public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Result>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<CheckoutBasketCommandHandler> _logger;
        private readonly IMediator _mediator;


        public async Task<Result> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {

            // Sepeti kontrol et
            // Ödeme işlemlerini gerçekleştir
            // Fatura bilgilerini kaydet
            // Sepeti temizle

            var basket = await _basketRepository.GetBasketAsync(request.UserId, cancellationToken);

            if (basket == null || !basket.Items.Any())
            {
                return Result.Failure("Sepet bulunamadı");
            }

            try
            {
                var domainEvent = new BasketCheckOutDomainEvent
                {
                    UserId = request.UserId,
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.EmailAddress,
                    BillingAddress = request.BillingAddress,
                    ShippingAddress = request.ShippingAddress,
                    PaymentMethod = request.PaymentMethod,
                    Items = basket.Items.Select(i => new BasketCheckoutItemEvent
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Price = i.Price,
                        Quantity = i.Quantity
                    }).ToList(),
                    TotalPrice = basket.GetTotalPrice()

                };
                await _mediator.Publish(domainEvent);  
                await _basketRepository.DeleteBasketAsync(request.UserId, cancellationToken);

                return Result.Success("Sepet başarıyla onaylandı ve temizlendi.");


            }
            catch (Exception)
            {

                _logger.LogError("Sepet onaylama işlemi sırasında hata oluştu.");
                return Result.Failure("Sepet onaylama işlemi sırasında hata oluştu.");

            }

        }
    }
  
}
