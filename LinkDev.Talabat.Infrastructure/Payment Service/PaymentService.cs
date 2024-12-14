using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Order;
using LinkDev.Talabat.Shared.Models;
using Microsoft.Extensions.Options;
using Stripe;
using Product = LinkDev.Talabat.Core.Domain.Entities.Products.Product;

namespace LinkDev.Talabat.Infrastructure.Payment_Service
{
    internal class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IOptions<RedisSettings> redisSettings) : IPaymentService
    {
        private readonly RedisSettings _redisSettings = redisSettings.Value;

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await basketRepository.GetAsync(basketId);

            //if (basket is null) throw NotFoundException(nameof(CustomerBasket), basketId);

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.getRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                //if (deliveryMethod is null) throw NotFoundException(nameof(deliveryMethod), basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod!.Cost;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = unitOfWork.getRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    //if (product is null) throw NotFoundException(nameof(Product), item.Id);
                    if (item.Price != product!.Price)
                        item.Price = product.Price;
                }
            }

            PaymentIntent paymentIntent = null;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) // Create NEW Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "Card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);  // Integration With Stripe
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Updated an Existing Payment Intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);  // Integration With Stripe
            }

            await basketRepository.UpdateAsync(basket, TimeSpan.FromDays(_redisSettings.TimeToLiveInDays));

            return basket;
        }
    }
}
