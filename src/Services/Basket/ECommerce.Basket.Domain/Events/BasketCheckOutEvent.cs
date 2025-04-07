using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Domain.Events
{
    public record BasketCheckOutDomainEvent : INotification
    {
        public string UserId { get; init; }
        public string UserName { get; init; }
        public decimal TotalPrice { get; init; }

        //Fatura bilgileri:
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string BillingAddress { get; init; }
        public string ShippingAddress { get; init; }

        //Ödeme bilgileri:
        public string PaymentMethod { get; init; }

        public List<BasketCheckoutItemEvent> Items { get; init; }



    }
}
