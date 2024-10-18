using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var storeContextInitializer = services.GetRequiredService<IStoreDbInitializer>();
            var storeIdentityContextInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();
            //Ask Runtime Environment for an Object from "StoreContext" Service Explicitly.

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync();

                await storeIdentityContextInitializer.InitializeAsync();
                await storeIdentityContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has occurred during applying the migrations or the data seeding.");
            }

            /// try
            /// {
            ///     var penddingMigrations = StoreContext.Database.GetPendingMigrations();
            /// 
            ///     if(penddingMigrations.Any())
            ///         await StoreContext.Database.MigrateAsync();      // Update-Database 
            /// }
            /// catch(Exception ex)
            /// {
            /// 
            /// }
            /// finally
            /// {
            ///     await StoreContext.DisposeAsync();
            /// } 

            return app;       
        }
    }
}
