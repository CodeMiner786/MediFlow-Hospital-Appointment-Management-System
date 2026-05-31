public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];

    public ApiResponseDto() { } // serializer-friendly

    private ApiResponseDto(T? data, bool success, string message, List<string>? errors)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors ?? [];
    }

    public static ApiResponseDto<T> SuccessResponse(T data, string message = "Success")
        => new(data, true, message, null);

    public static ApiResponseDto<T> FailResponse(string message, List<string>? errors = null)
        => new(default, false, message, errors);
}
