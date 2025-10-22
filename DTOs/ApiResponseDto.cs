namespace AutoKitApi.DTOs;

public class ApiResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }

    public static ApiResponseDto SuccessResponse<T>(T data, string message = "")
    {
        return new ApiResponseDto { Success = true, Message = message, Data = data };
    }
    
    public static ApiResponseDto SuccessResponse(string message = "")
    {
        return new ApiResponseDto { Success = true, Message = message};
    }
    
    public static ApiResponseDto FailResponse(string message)
    {
        return new ApiResponseDto{ Success = false, Message = message };
    }
}