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
                    Title = "Beklenmeyen Hata",
                    Detail = "Sonuç nesnesi null olarak döndü.",
                    ErrorMessage = "Sonuç nesnesi işlenemedi."
                }, statusCode: StatusCodes.Status500InternalServerError);
            }

            if (result.IsSuccess)
                return Results.Ok(result.Data);

            var errorResponse = new ErrorResponseDto
            {
                Status = result.StatusCode,
                Title = result.Title ?? GetTitleByStatusCode(result.StatusCode),
                Detail = result.Message ?? "İstek işlenirken bir hata oluştu.",
                ErrorMessage = result.Message
            };

            return Results.Json(errorResponse, statusCode: result.StatusCode);
        }

        private static string GetTitleByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "Geçersiz İstek",
                StatusCodes.Status401Unauthorized => "Yetkisiz Erişim",
                StatusCodes.Status403Forbidden => "Erişim Yasak",
                StatusCodes.Status404NotFound => "Bulunamadı",
                StatusCodes.Status422UnprocessableEntity => "İşlenemeyen İçerik",
                StatusCodes.Status500InternalServerError => "Sunucu Hatası",
                _ => "Hata"
            };
        }
    }
}
