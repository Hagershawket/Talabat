using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Orders;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            // services.AddScoped(typeof(Func<IBasketService>), typeof(Func<BasketService>));

            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                //var mapper = serviceProvider.GetRequiredService<IMapper>();
                //var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                //var basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();

                //return () => new BasketService(basketRepository, mapper, configuration);

                return () => serviceProvider.GetRequiredService<IBasketService>();
            });

            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });

            return services;
        }
    }
}
