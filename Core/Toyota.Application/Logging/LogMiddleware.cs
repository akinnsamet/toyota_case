using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog.Context;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Toyota.Application.Api;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Logging;
using Toyota.Shared.Utilities;

namespace Toyota.Application.Logging
{
    public class LogMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var currentUser = ApiConfiguration.CurrentUser;
                var properties = new List<IDisposable>
                {
                    LogContext.PushProperty("Channel", currentUser.Channel),
                    LogContext.PushProperty("UserId", currentUser.Id),
                };

                using (new CompositeDisposable(properties))
                {
                    await LogRequest(context);
                    await LogResponse(context);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context,ex);
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            string?[]? encryptMethods = ApiConfiguration.Configuration?.GetSection("EncryptMethods")?.Get<string?[]?>();
            if (context.Request.Path.Value != null && encryptMethods!=null && encryptMethods.Contains(context.Request.Path.Value))
            {
                body = Encryptions.EncryptText(body);
            }
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            //var logText =
            //     $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] REQUEST\n" +
            //     $"Schema: {context.Request.Scheme}\n" +
            //     $"Host: {context.Request.Host}\n" +
            //     $"Path: {context.Request.Path}\n" +
            //     $"QueryString: {context.Request.QueryString}\n" +
            //     //$"Request Body: {body}\n" +
            //     $"UserId: {ApiConfiguration.CurrentUser?.Id ?? 0}\n" +
            //     $"----------------------------------------\n";

            var logText = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] REQUEST Path: {context.Request.Path} UserId :  {ApiConfiguration.CurrentUser?.Id ?? 0}\n";
            await AppendLogAsync(logText);
           
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await next(context);
                
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                //var logText =
                //    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] RESPONSE\n" +
                //    $"Schema: {context.Request.Scheme}\n" +
                //    $"Host: {context.Request.Host}\n" +
                //    $"Path: {context.Request.Path}\n" +
                //    $"QueryString: {context.Request.QueryString}\n" +
                //    //$"Response Body: {text}\n" +
                //    $"UserId: {ApiConfiguration.CurrentUser?.Id ?? 0}\n" +
                //    $"----------------------------------------\n";

                //await AppendLogAsync(logText);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

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

        private async Task AppendLogAsync(string logText)
        {
            try
            {
                var path = Path.Combine("/app/Data", "applicationLogs.txt");
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory!);

                await File.AppendAllTextAsync(path, logText);
            }
            catch
            {}
        }
    }
}
