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
    public sealed class GetDoctorDashboardRangeQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetDoctorDashboardRangeQuery, ApiResponseDto<PagedResultDto<DoctorDashboardResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorDashboardResponseDto>>> Handle(
            GetDoctorDashboardRangeQuery request,
            CancellationToken cancellationToken)
        {
            var items = new List<DoctorDashboardResponseDto>();

            await foreach (var metric in unitOfWork.DoctorDashboardMetrics
                .GetDoctorMetricsRangeStream(request.DoctorId, request.StartDate, request.EndDate)
                .WithCancellation(cancellationToken))
            {
                items.Add(mapper.Map<DoctorDashboardResponseDto>(metric));
            }

            var paged = new PagedResultDto<DoctorDashboardResponseDto>(
                items.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                items.Count,
                request.PageNumber,
                request.PageSize);

            return ApiResponseDto<PagedResultDto<DoctorDashboardResponseDto>>.Ok(paged);
        }
    }

}
