using App.Core.DTO;
using System.Net;
using System.Text.Json;

namespace App.MinimalApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            TypeInfoResolver = AppJsonSerializerContext.Default,
            WriteIndented = true
        };

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new ErrorResponseDto
                {
                    Status = 500,
                    Title = "Sunucu Hatası",
                    Detail = "İstek işlenirken beklenmeyen bir hata oluştu.",
                    ErrorMessage = ex.Message
                };

                var json = JsonSerializer.Serialize(
                    errorResponse,
                    AppJsonSerializerContext.Default.ErrorResponseDto
                );

                await context.Response.WriteAsync(json);
            }
        }
    }
}