using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Identity
{
    public record RevokeRoleCommand(Guid TargetUserId, Guid AdminUserId, string? Reason)
    : IRequest<ApiResponse<bool>>;

}
