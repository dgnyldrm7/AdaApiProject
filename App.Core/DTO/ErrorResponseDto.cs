﻿namespace App.Core.DTO
{
    public class ErrorResponseDto
    {
        public int Status { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
