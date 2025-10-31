using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Extensions;
using System.Net;
using System.Text.Json;
using Toyota.Shared.Logging;
using Toyota.Shared.Utilities;

namespace Toyota.Application.Logging
{
    public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            logger.LogError(exception, "GlobalException : {ex}", ErrorHelper.GetExceptionString(exception));

            var response = new ServiceResponse
            {
                Result = new ServiceResult
                {
                    ResultCode = context.Response.StatusCode,
                    ResultMessage = Constants.ExceptionErrorMessage + " - " + Activity.Current?.GetTraceId()
                }
            };

            switch (exception)
            {
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Result.ResultCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Result.ResultCode = (int)HttpStatusCode.BadRequest;
                    break;
            }

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
