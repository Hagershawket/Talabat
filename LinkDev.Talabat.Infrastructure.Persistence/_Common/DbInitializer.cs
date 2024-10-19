using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
    internal abstract class DbInitializer(DbContext _dbContext) : IDbInitializer
    {
        public virtual async Task InitializeAsync()
        {
            var penddingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (penddingMigrations.Any())
                await _dbContext.Database.MigrateAsync();      // Update-Database 
        }

        public abstract Task SeedAsync();
    }
}
