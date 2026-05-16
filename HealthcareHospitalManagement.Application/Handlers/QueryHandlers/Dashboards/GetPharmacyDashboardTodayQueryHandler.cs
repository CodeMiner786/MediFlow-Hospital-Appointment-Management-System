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
    public sealed class GetPharmacyDashboardTodayQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetPharmacyDashboardTodayQuery, ApiResponseDto<PharmacyDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<PharmacyDashboardResponseDto>> Handle(
            GetPharmacyDashboardTodayQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.PharmacyDashboardMetrics.GetTodayPharmacyMetricsAsync(request.PharmacyProfileId, cancellationToken);
            if (metric is null)
                return ApiResponseDto<PharmacyDashboardResponseDto>.Fail("আজকের Pharmacy ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<PharmacyDashboardResponseDto>.Ok(mapper.Map<PharmacyDashboardResponseDto>(metric));
        }
    }

}
