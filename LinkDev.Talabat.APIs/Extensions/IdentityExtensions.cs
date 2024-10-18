using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

            services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
            {
                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                // identityOptions.Password.RequireNonAlphanumeric = true;  // $#@%
                // identityOptions.Password.RequiredUniqueChars = 2;
                // identityOptions.Password.RequiredLength = 6;
                // identityOptions.Password.RequireDigit = true;
                // identityOptions.Password.RequireLowercase = true;
                // identityOptions.Password.RequireUppercase = true;

                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz1234567890-_@#$";

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

                //identityOptions.Stores
                //identityOptions.Tokens
                //identityOptions.ClaimsIdentity
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}
