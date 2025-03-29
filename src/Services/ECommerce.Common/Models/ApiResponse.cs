using ECommerce.Common.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Common.Models
{
    public  class ApiResponse<T>
    {
        //nullable T Data:
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PaginationMetadata PaginationMetadata { get; set; }

        private ApiResponse() { }

        public static ApiResponse<T> FromResult(Result<T> result)
        {
            return new ApiResponse<T>
            {
                Data = result.Data,
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Errors = result.Errors
            };
        }

        //Success response:
        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }

        //Success response with pagination:
        public static ApiResponse<T> Success(T data, PaginationMetadata paginationMetadata, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                PaginationMetadata = paginationMetadata
            };
        }

        //Failure response:
        public static ApiResponse<T> Failure(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message
            };
        }

        //Failure response with errors:
        public static ApiResponse<T> Failure(string message, IEnumerable<string> errors)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }


        //Failure response with errors withouth message:
        public static ApiResponse<T> Failure(IEnumerable<string> errors)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }



    }
}
