using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    internal sealed class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext, UserManager<ApplicationUser> userManager) : DbInitializer(_dbContext), IStoreIdentityDbInitializer
    {
        public override async Task SeedAsync()
        {
            var user = new ApplicationUser
            {
                DisplayName = "Hager Shawkat",
                UserName = "hager.shawkat",
                Email = "hagershawkat@gmail.com",
                PhoneNumber = "01122334455",                
            };

            await userManager.CreateAsync(user, "P@ssw0rd");
        }
    }
}
