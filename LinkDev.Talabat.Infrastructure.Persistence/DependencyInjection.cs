using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Store DbContext

            services.AddDbContext<StoreDbContext>((serviceProvider, optionBuilder) =>
            {
            optionBuilder.UseLazyLoadingProxies()
                                 .AddInterceptors(serviceProvider.GetRequiredService<BaseAuditableEntityInterceptor>())
                                 .UseSqlServer(configuration.GetConnectionString("StoreContext"));
                }/*, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped*/);

            services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();

            services.AddScoped(typeof(BaseAuditableEntityInterceptor));

            // services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));

            #endregion

            #region Identity DbContext

            services.AddDbContext<StoreIdentityDbContext>((optionBuilder) =>
            {
                optionBuilder.UseLazyLoadingProxies()
                             .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            }/*, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped*/);

            services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));

            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return services;
        }
    }
}
