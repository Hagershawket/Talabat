using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Dashboard.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
            {
                identityOptions.User.RequireUniqueEmail = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Login";
                options.LogoutPath = "/Admin/Logout";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            return services;
        }
    }
}
