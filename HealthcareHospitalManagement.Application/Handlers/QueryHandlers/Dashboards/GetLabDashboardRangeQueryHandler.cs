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
    public sealed class GetLabDashboardRangeQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetLabDashboardRangeQuery, ApiResponseDto<PagedResultDto<LabDashboardResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<LabDashboardResponseDto>>> Handle(
            GetLabDashboardRangeQuery request,
            CancellationToken cancellationToken)
        {
            var items = new List<LabDashboardResponseDto>();

            await foreach (var metric in unitOfWork.LabDashboardMetrics
                .GetLabMetricsRangeStream(request.LabProfileId, request.StartDate, request.EndDate)
                .WithCancellation(cancellationToken))
            {
                items.Add(mapper.Map<LabDashboardResponseDto>(metric));
            }

            var paged = new PagedResultDto<LabDashboardResponseDto>(
                items.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                items.Count,
                request.PageNumber,
                request.PageSize);

            return ApiResponseDto<PagedResultDto<LabDashboardResponseDto>>.Ok(paged);
        }
    }

}
