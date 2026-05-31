namespace HealthcareHospitalManagement.Domain.Common.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; }
        public string? Message { get; }
        public T? Data { get; }
        public IReadOnlyList<string> Errors { get; }

        private ApiResponse(T? data, bool success, string? message, List<string>? errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = (errors ?? new List<string>()).AsReadOnly();
        }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
            => new(data, true, message, null);

        public static ApiResponse<T> FailResponse(string message, List<string>? errors = null)
            => new(default, false, message, errors);
    }
}
