using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Identity
{
    public record GetAllUsersQuery(UserRole? Role, int PageNumber, int PageSize)
    : IRequest<ApiResponse<PagedResultDto<UserProfileResponseDto>>>;

}
