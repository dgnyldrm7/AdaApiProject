using App.Core.DTO;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace App.MinimalApi.Middlewares
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponseDto
                {
                    Status = 400,
                    Title = "Doğrulama Hatası",
                    Detail = "Bir veya daha fazla doğrulama hatası oluştu.",
                    ErrorMessage = string.Join(" | ", ex.Errors.Select(e => e.ErrorMessage))
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
