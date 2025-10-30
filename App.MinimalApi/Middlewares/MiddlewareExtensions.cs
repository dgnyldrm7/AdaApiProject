namespace App.MinimalApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }

        public static IApplicationBuilder UseValidationExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationExceptionMiddleware>();
        }
    }
}