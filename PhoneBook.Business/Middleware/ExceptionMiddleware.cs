using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PhoneBook.Core.Error;
using System.Net;

namespace PhoneBook.Business.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // İsteği bir sonraki durağa gönder
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Hata oluştu! Burada yakalıyoruz.
                _logger.LogError($"Bir hata oluştu: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorresponse = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sunucu tarafında beklenmedik bir hata oluştu.",
                Detail = exception.Message // Gerçek projelerde bu sadece Debug modda gösterilir
            };

            var jsonReEsponse = System.Text.Json.JsonSerializer.Serialize(errorresponse);

            return context.Response.WriteAsync(jsonReEsponse);
        }
    }
}
