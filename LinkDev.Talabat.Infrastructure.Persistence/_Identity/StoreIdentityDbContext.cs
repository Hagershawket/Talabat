using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());

            //builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
            //    type => type.Namespace!.Contains("LinkDev.Talabat.Infrastructure.Persistence._Identity.Config"));

            //builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
            //    type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));
        }
    }
}
