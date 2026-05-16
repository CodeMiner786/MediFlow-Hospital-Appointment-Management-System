using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Application.Queries.Dashboards;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Dashboards
{
    public sealed class GetPharmacyDashboardRangeQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetPharmacyDashboardRangeQuery, ApiResponseDto<PagedResultDto<PharmacyDashboardResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<PharmacyDashboardResponseDto>>> Handle(
            GetPharmacyDashboardRangeQuery request,
            CancellationToken cancellationToken)
        {
            var items = new List<PharmacyDashboardResponseDto>();

            await foreach (var metric in unitOfWork.PharmacyDashboardMetrics
                .GetPharmacyMetricsRangeStream(request.PharmacyProfileId, request.StartDate, request.EndDate)
                .WithCancellation(cancellationToken))
            {
                items.Add(mapper.Map<PharmacyDashboardResponseDto>(metric));
            }

            var paged = new PagedResultDto<PharmacyDashboardResponseDto>(
                items.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                items.Count,
                request.PageNumber,
                request.PageSize);

            return ApiResponseDto<PagedResultDto<PharmacyDashboardResponseDto>>.Ok(paged);
        }
    }

}
