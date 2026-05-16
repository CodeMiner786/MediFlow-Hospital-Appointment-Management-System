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
    public sealed class GetLabDashboardTodayQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetLabDashboardTodayQuery, ApiResponseDto<LabDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<LabDashboardResponseDto>> Handle(
            GetLabDashboardTodayQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.LabDashboardMetrics.GetTodayLabMetricsAsync(request.LabProfileId, cancellationToken);
            if (metric is null)
                return ApiResponseDto<LabDashboardResponseDto>.Fail("আজকের Lab ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<LabDashboardResponseDto>.Ok(mapper.Map<LabDashboardResponseDto>(metric));
        }
    }

}
