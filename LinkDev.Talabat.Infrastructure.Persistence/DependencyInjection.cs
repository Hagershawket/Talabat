using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>((optionBuilder) =>
            {
                optionBuilder.UseLazyLoadingProxies()
                             .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            }/*, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped*/);

            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();

            // services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return services;
        }
    }
}
