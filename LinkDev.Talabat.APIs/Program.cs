
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

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

            webApplicationbuilder.Services.AddControllers()
                                          .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly)
                                          .ConfigureApiBehaviorOptions((options) =>
                                          {
                                              options.SuppressModelStateInvalidFilter = false;
                                              options.InvalidModelStateResponseFactory = (actionContext) =>
                                              {
                                                  var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                                                                       .SelectMany(P => P.Value!.Errors)
                                                                                       .Select(P => P.ErrorMessage);

                                                  return new BadRequestObjectResult(new ApiValidationErrorResponse()
                                                  {
                                                      Errors = errors
                                                  });
                                              };
                                          });

            // Another Way to configure "InvalidModelStateResponseFactory"
            /// webApplicationbuilder.Services.Configure<ApiBehaviorOptions>((options) =>
            /// {
            ///     options.SuppressModelStateInvalidFilter = false;
            ///     options.InvalidModelStateResponseFactory = (actionContext) =>
            ///     {
            ///         var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
            ///                                              .SelectMany(P => P.Value!.Errors)
            ///                                              .Select(P => P.ErrorMessage);
            /// 
            ///         return new BadRequestObjectResult(new ApiValidationErrorResponse()
            ///         {
            ///             Errors = errors
            ///         });
            ///     };
            /// });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationbuilder.Services.AddEndpointsApiExplorer();
            webApplicationbuilder.Services.AddSwaggerGen();

            webApplicationbuilder.Services.AddPersistenceServices(webApplicationbuilder.Configuration);

            webApplicationbuilder.Services.AddApplicationServices();

            webApplicationbuilder.Services.AddInfrastructureService(webApplicationbuilder.Configuration);

            webApplicationbuilder.Services.AddScoped<CustomExceptionHandlerMiddleware>();

            // webApplicationbuilder.Services.AddHttpContextAccessor();
            // webApplicationbuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

            #endregion

            var app = webApplicationbuilder.Build();

            #region Databases Initialization

            await app.InitializeStoreContextAsync();

            #endregion

            #region Configure Kestrel Middlewares

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseStaticFiles();

            app.UseAuthentication()
               .UseAuthorization();

            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
