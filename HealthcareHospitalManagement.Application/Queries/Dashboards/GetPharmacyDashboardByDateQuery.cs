using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Dashboards
{
    public record GetPharmacyDashboardByDateQuery(
    Guid PharmacyProfileId,
    DateOnly Date
    ) : IRequest<ApiResponseDto<PharmacyDashboardResponseDto>>;

}
