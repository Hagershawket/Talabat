using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, Func<IBasketService> BasketServiceFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(BasketServiceFactory);
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
