using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using System.Net;
using System.Text.Json;

namespace MediFlow_API.Middlewares.ExceptionThrow
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal Server Error from Middleware.";

            if (ex is InvalidOperationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = ex.Message;
            }
            else if (ex is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = ex.Message;
            }
            else if (ex is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = ex.Message;
            }

            context.Response.StatusCode = statusCode;

            // ✅ Inner Exception সহ error list — debug এর জন্য
            var errors = new List<string>();

            if (_env.IsDevelopment())
            {
                errors.Add(ex.Message);

                if (ex.InnerException != null)
                    errors.Add($"Inner: {ex.InnerException.Message}");

                if (ex.InnerException?.InnerException != null)
                    errors.Add($"Inner2: {ex.InnerException.InnerException.Message}");
            }
            else
            {
                errors.Add("An unexpected error occurred.");
            }

            var response = ApiResponse<object>.FailResponse(message, errors);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}