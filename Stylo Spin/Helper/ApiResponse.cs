﻿namespace Stylo_Spin.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new ApiResponse<T>(true, message, data);

        public static ApiResponse<T> ErrorResponse(string message)
            => new ApiResponse<T>(false, message);
    }
}
