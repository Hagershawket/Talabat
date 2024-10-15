﻿using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.Middlewares
{
    // Convension-Based
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // Logic Excuted with the Request

                await _next(httpContext);

                // Logic Excuted with the Response

                /// if(httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                /// {
                ///     var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The requested endpoint:{httpContext.Request.Path} is not found.");
                ///     await httpContext.Response.WriteAsync(response.ToString());
                /// }
            }
            catch (Exception ex)
            {
                ApiResponse response;

                switch (ex) 
                {
                    case NotFoundException:

                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        httpContext.Response.ContentType = "application/json";
                        response = new ApiResponse(404, ex.Message);
                        await httpContext.Response.WriteAsync(response.ToString());

                        break;

                    default:


                        if (_env.IsDevelopment())
                        {
                            // Development Mode
                            _logger.LogError(ex, ex.Message);
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());
                        }
                        else
                        {
                            // Production Mode
                            /// Log Exception Details in Database | File (Text, Json)
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                        }

                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        httpContext.Response.ContentType = "application/json";

                        await httpContext.Response.WriteAsync(response.ToString());
                        break;
                }
            }

        }
    }
}