using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using System.Collections.Generic;

namespace HealthcareHospitalManagement.Application.Helpers.MapperService
{
    public static class ApiResponseMapper
    {
        public static ApiResponseDto<T> ToDto<T>(ApiResponse<T> response)
        {
            return response.Success
                ? ApiResponseDto<T>.SuccessResponse(response.Data!, response.Message ?? "Success")
                : ApiResponseDto<T>.FailResponse(response.Message ?? "Failed", [.. response.Errors]);
        }

        public static PagedResultDto<T> ToPagedResultDto<T>(PagedResponse<T> response)
        {
            return new PagedResultDto<T>(
                response.Data,
                response.TotalRecords,
                response.PageNumber,
                response.PageSize
            );
        }
    }
}
