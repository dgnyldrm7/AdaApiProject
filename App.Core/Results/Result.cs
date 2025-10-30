namespace App.Core.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public int StatusCode { get; init; }
        public string? Title { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }

        private Result(bool isSuccess, int statusCode, string? title, string? message, T? data)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Title = title;
            Message = message;
            Data = data;
        }

        public static Result<T> Success(T data, string? message = null)
            => new(true, 200, "Success", message, data);

        public static Result<T> Fail(int statusCode, string title, string message)
            => new(false, statusCode, title, message, default);
    }
}