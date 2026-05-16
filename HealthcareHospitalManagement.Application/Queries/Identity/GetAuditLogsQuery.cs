using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Identity
{
    public record GetAuditLogsQuery(AuditLogFilterRequestDto RequestDto)
    : IRequest<ApiResponse<PagedResultDto<AuditLogResponseDto>>>;

}
