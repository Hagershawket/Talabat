using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Common.Services.Attachments;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Dashboard.Mapping;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LinkDev.Talabat.Dashboard
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDashboardDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>((serviceProvider, optionBuilder) =>
            {
                optionBuilder.AddInterceptors(serviceProvider.GetRequiredService<BaseAuditableEntityInterceptor>())
                             .UseSqlServer(configuration.GetConnectionString("StoreContext"));
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
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            services.AddScoped(typeof(IAttachmentService), typeof(AttachmentService));

            services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            services.AddHttpContextAccessor();
            services.AddScoped<BaseAuditableEntityInterceptor>();

            services.AddAutoMapper(typeof(MvcMappingProfile).Assembly);
            services.AddScoped(typeof(IAttachmentService), typeof(AttachmentService));

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAuthService>();
            });
            return services;
        }
    }
}
