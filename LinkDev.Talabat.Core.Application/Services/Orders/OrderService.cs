using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Order;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    internal class OrderService(IMapper mapper, IUnitOfWork unitOfWork, BasketService basketService) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // 1. Get Basket From Baskets Repo

            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

            // 2. Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.getRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is not null)
                    {
                        var productItemOrdered = new ProductItemOrdered()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? "",
                        };

                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            // 3. Calculate Subtotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Map Address

            var address = mapper.Map<Address>(order.ShippingAddress);

            // 5. Create Order

            var orderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                Items = orderItems,
                SubTotal = subTotal,
                DeliveryMethodId = order.DeliveryMethodId,
            };

            await unitOfWork.getRepository<Order, int>().AddAsync(orderToCreate);

            // 6. Save To Database

            var created = await unitOfWork.CompleteAysnc() > 0;

            if (!created) throw new BadRequestException("an error has accured during creating the order");

            return mapper.Map<OrderToReturnDto>(orderToCreate);
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

    }
}
