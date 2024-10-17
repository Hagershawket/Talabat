﻿using LinkDev.Talabat.Core.Domain.Infrastructure;
using LinkDev.Talabat.Infrastructure.Basket_Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return services;
        }
    }
}
