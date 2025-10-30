using App.Core.DTO;
using App.Core.Results;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace App.Services.Extension
{
    public static class ResultsExtensions
    {
        public static IResult FromResult<T>(this IResultExtensions _, Result<T> result)
        {
            if (result == null)
            {
                return Results.Json(new ErrorResponseDto
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Unexpected Error",
                    Detail = "Result object was null.",
                    ErrorMessage = "Result object could not be processed."
                }, statusCode: StatusCodes.Status500InternalServerError);
            }

            if (result.IsSuccess)
                return Results.Ok(result.Data);

            var errorResponse = new ErrorResponseDto
            {
                Status = result.StatusCode,
                Title = result.Title ?? GetTitleByStatusCode(result.StatusCode),
                Detail = result.Message ?? "An error occurred while processing your request.",
                ErrorMessage = result.Message
            };

            return Results.Json(errorResponse, statusCode: result.StatusCode);
        }

        private static string GetTitleByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status422UnprocessableEntity => "Unprocessable Content",
                StatusCodes.Status500InternalServerError => "Internal Server Error",
                _ => "Error"
            };
        }
    }
}
