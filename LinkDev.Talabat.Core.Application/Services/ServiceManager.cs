using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Orders;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IOrderService> _orderService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, Func<IBasketService> BasketServiceFactory, Func<IAuthService> AuthServiceFactory, Func<IOrderService> orderServiceFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(BasketServiceFactory);
            _authService = new Lazy<IAuthService>(AuthServiceFactory);
            _orderService = new Lazy<IOrderService>(orderServiceFactory);
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
