using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDashboardDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>((optionBuilder) =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            return services;
        }


        public static IServiceCollection AddIdentityDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreIdentityDbContext>((optionBuilder) =>
            {
                optionBuilder.UseLazyLoadingProxies()
                             .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            });

            return services;
        }
    }
}
