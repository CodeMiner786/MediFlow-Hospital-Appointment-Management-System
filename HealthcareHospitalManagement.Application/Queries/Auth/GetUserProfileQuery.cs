using HealthcareHospitalManagement.Application.DTOs.Auth;
using MediatR;

using System;

namespace HealthcareHospitalManagement.Application.Queries.Auth
{
    // Get Profile
    public record GetUserProfileQuery(Guid UserId) : IRequest<UserProfileResponseDto>;
}
