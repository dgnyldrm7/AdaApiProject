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
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new ErrorResponseDto
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while processing your request.",
                    ErrorMessage = ex.Message
                };

                var json = JsonSerializer.Serialize(errorResponse, _jsonOptions);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
