using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction.Services.Basket.Models
{
    public class CustomerBasketDto
    {
        [Required]
        public required string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
