namespace ApplicationLayer.Commands
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public ApiError Error { get; set; }

        public static ApiResponse<T> CreateSuccess(T data)
        {
            return new ApiResponse<T>
            {
                Data = data,
                Success = true,
                Error = null
            };
        }
        public static ApiResponse<T> CreateNoContent()
        {
            return new ApiResponse<T>
            {
                Data = default,
                Success = true,
                Error = null
            };
        }

        public static ApiResponse<T> CreateNotFound(string message)
        {
            return new ApiResponse<T>
            {
                Data = default,
                Success = false,
                Error = new ApiError
                {
                    Code = 404,
                    Message = message
                }
            };
        }

        public static ApiResponse<T> CreateBadRequest(string message)
        {
            return new ApiResponse<T>
            {
                Data = default,
                Success = false,
                Error = new ApiError
                {
                    Code = 400,
                    Message = message
                }
            };
        }

        public static ApiResponse<T> CreateError(string message)
        {
            return new ApiResponse<T>
            {
                Data = default,
                Success = false,
                Error = new ApiError
                {
                    Code = 500,
                    Message = message
                }
            };
        }
    }

    public class ApiError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public object ValidationErrors { get; set; }
    }

}
