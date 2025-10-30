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

                var error = new ErrorResponseDto
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred.",
                    ErrorMessage = string.Join(" | ", ex.Errors.Select(e => e.ErrorMessage))
                };

                var json = JsonSerializer.Serialize(error);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
