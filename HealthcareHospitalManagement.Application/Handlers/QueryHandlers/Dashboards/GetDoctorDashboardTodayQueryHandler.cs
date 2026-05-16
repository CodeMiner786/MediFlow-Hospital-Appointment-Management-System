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
    public sealed class GetDoctorDashboardTodayQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetDoctorDashboardTodayQuery, ApiResponseDto<DoctorDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<DoctorDashboardResponseDto>> Handle(
            GetDoctorDashboardTodayQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.DoctorDashboardMetrics.GetTodayDoctorMetricsAsync(request.DoctorId, cancellationToken);
            if (metric is null)
                return ApiResponseDto<DoctorDashboardResponseDto>.Fail("আজকের Doctor ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<DoctorDashboardResponseDto>.Ok(mapper.Map<DoctorDashboardResponseDto>(metric));
        }
    }

}
