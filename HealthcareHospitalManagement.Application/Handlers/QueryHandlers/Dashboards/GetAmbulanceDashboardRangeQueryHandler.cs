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
    public sealed class GetAmbulanceDashboardRangeQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetAmbulanceDashboardRangeQuery, ApiResponseDto<PagedResultDto<AmbulanceDashboardResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<AmbulanceDashboardResponseDto>>> Handle(
            GetAmbulanceDashboardRangeQuery request,
            CancellationToken cancellationToken)
        {
            var items = new List<AmbulanceDashboardResponseDto>();

            await foreach (var metric in unitOfWork.AmbulanceDashboardMetrics
                .GetProviderMetricsRangeStream(request.ProviderId, request.StartDate, request.EndDate)
                .WithCancellation(cancellationToken))
            {
                items.Add(mapper.Map<AmbulanceDashboardResponseDto>(metric));
            }

            var paged = new PagedResultDto<AmbulanceDashboardResponseDto>(
                items.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                items.Count,
                request.PageNumber,
                request.PageSize);

            return ApiResponseDto<PagedResultDto<AmbulanceDashboardResponseDto>>.Ok(paged);
        }
    }

}
