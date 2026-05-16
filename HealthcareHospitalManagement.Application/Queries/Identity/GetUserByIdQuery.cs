using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Identity
{
    public record GetUserByIdQuery(Guid UserId)
    : IRequest<ApiResponse<UserProfileResponseDto>>;

}
