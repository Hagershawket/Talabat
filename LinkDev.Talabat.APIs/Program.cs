
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        //[FromServices]
        //public static StoreContext StoreContext { get; set; } = null!;
        public static async Task Main(string[] args)
        {
            var webApplicationbuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.

            webApplicationbuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationbuilder.Services.AddEndpointsApiExplorer();
            webApplicationbuilder.Services.AddSwaggerGen();

            webApplicationbuilder.Services.AddPersistenceServices(webApplicationbuilder.Configuration);

            #endregion

            var app = webApplicationbuilder.Build();

            #region Update Database and Seeding

            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<StoreContext>();
            //Ask Runtime Environment for an Object from "StoreContext" Service Explicitly.

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var penddingMigrations = dbContext.Database.GetPendingMigrations();

                if (penddingMigrations.Any())
                    await dbContext.Database.MigrateAsync();      // Update-Database 

                await StoreContextSeed.SeedAsync(dbContext);
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
            

	        #endregion

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
