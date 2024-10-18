using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
    // 1. Convension-Based
    // 2. Factory-Based
    // 3. Request Delegate
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        //private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(/*RequestDelegate next,*/ ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            //_next = next;
            _logger = logger;
            _env = env;
        }


        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
        {
            try
            {
                // Logic Excuted with the Request

                await _next(httpContext);

                // Logic Excuted with the Response

            }
            catch (Exception ex)
            {
                #region Logging : TODO

                if (_env.IsDevelopment())
                {
                    // Development Mode
                    _logger.LogError(ex, ex.Message);
                }
                else
                {
                    // Production Mode
                    /// Log Exception Details in Database | File (Text, Json)
                }

                #endregion

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;

            switch (ex)
            {
                case NotFoundException:

                    httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse(404, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());

                    break;

                case BadRequestException:

                    httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse(400, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());

                    break;

                case UnAuthorizedException:

                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse(401, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());

                    break;

                default:
                    response = _env.IsDevelopment() ? new ApiExceptionResponse((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) :
                                                      new ApiExceptionResponse((int) HttpStatusCode.InternalServerError);
 

                    httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
            }
        }
    }
}
