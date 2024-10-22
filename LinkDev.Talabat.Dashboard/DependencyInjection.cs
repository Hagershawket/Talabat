using LinkDev.Talabat.Core.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Dashboard.Mapping;
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

        public static IServiceCollection AddDashboardServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MvcMappingProfile).Assembly);
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAuthService>();
            });
            return services;
        }
    }
}
