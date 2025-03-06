using Serilog;
using System.Text;

namespace DemoNetCore8Api.Service.Serilog
{
    public static class UseSerilogLoggingExtensions
    {
        public static void UseCustomSerilogRequestLogging(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var startTime = DateTime.UtcNow;

                // 捕获 Request Body
                string requestBody = string.Empty;
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering(); // 允许重复读取 Body
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                    {
                        requestBody = await reader.ReadToEndAsync();
                        context.Request.Body.Position = 0; // 重置 Body 流位置
                    }
                }

                // 替换 Response Body 为 MemoryStream
                var originalResponseBodyStream = context.Response.Body;
                using var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                try
                {
                    await next();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    var endTime = DateTime.UtcNow;
                    var processingTime = (endTime - startTime).TotalSeconds;

                    var statusCode = context.Response.StatusCode;

                    Log.ForContext("IsCustomLog", true)
                       .ForContext("ProcessingTime", processingTime)
                       .ForContext("StatusCode", statusCode)
                       .Information(
                            "RequestPath: {RequestPath}, RequestBody: {RequestBody}, ResponseBody: {ResponseBody}, StatusCode: {StatusCode}, ProcessingTime: {ProcessingTime}s",
                            context.Request.Path,
                            requestBody,
                            responseBody,
                            statusCode,
                            processingTime);

                    await responseBodyStream.CopyToAsync(originalResponseBodyStream);
                }
                catch (Exception ex)
                {
                    var endTime = DateTime.UtcNow;
                    var processingTime = (endTime - startTime).TotalSeconds;
                    var statusCode = context.Response.StatusCode; 

                    Log.ForContext("IsCustomLog", true)
                       .ForContext("ProcessingTime", processingTime)
                       .ForContext("StatusCode", statusCode)
                       .Error(ex,
                           "An error occurred while processing the request. RequestPath: {RequestPath}, RequestBody: {RequestBody}, StatusCode: {StatusCode}, ProcessingTime: {ProcessingTime}s",
                           context.Request.Path,
                           requestBody,
                           statusCode,
                           processingTime);

                    throw;
                }
                finally
                {
                    context.Response.Body = originalResponseBodyStream;
                }
            });
        }
    }
}
