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
    public record GetAmbulanceDashboardRangeQuery(
    Guid ProviderId,
    DateOnly StartDate,
    DateOnly EndDate,
    int PageNumber = 1,
    int PageSize = 10
    ) : IRequest<ApiResponseDto<PagedResultDto<AmbulanceDashboardResponseDto>>>;

}
