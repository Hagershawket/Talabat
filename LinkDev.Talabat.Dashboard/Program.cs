using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Dashboard.Extensions;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence;

namespace LinkDev.Talabat.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDashboardDb(builder.Configuration)
                            .AddIdentityDb(builder.Configuration);

            builder.Services.AddIdentityServices();
            builder.Services.AddDashboardServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureService(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
