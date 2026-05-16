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
    public sealed class GetAdminDashboardRangeQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetAdminDashboardRangeQuery, ApiResponseDto<PagedResultDto<AdminDashboardResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<AdminDashboardResponseDto>>> Handle(
            GetAdminDashboardRangeQuery request,
            CancellationToken cancellationToken)
        {
            var items = new List<AdminDashboardResponseDto>();

            await foreach (var metric in unitOfWork.AdminDashboardMetrics
                .GetMetricsByDateRangeStream(request.StartDate, request.EndDate)
                .WithCancellation(cancellationToken))
            {
                items.Add(mapper.Map<AdminDashboardResponseDto>(metric));
            }

            var paged = new PagedResultDto<AdminDashboardResponseDto>(
                items.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                items.Count,
                request.PageNumber,
                request.PageSize);

            return ApiResponseDto<PagedResultDto<AdminDashboardResponseDto>>.Ok(paged);
        }
    }

}
