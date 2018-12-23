using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.ExceptionHandler
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpStatusCodeExceptionMiddleware>() ??
                      throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = ex.ContentType;

                await context.Response.WriteAsync(ex.Message);
            }

            catch (ArgumentNullException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = @"text/plain";

                await context.Response.WriteAsync(ex.Message);
            }

            catch (InvalidOperationException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = @"text/plain";

                await context.Response.WriteAsync(ex.Message);
            }

            catch (NullReferenceException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = @"text/plain";

                await context.Response.WriteAsync(ex.Message);
            }


            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = @"text/plain";

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}
