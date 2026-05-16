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
    public sealed class GetAmbulanceDashboardTodayQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetAmbulanceDashboardTodayQuery, ApiResponseDto<AmbulanceDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceDashboardResponseDto>> Handle(
            GetAmbulanceDashboardTodayQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.AmbulanceDashboardMetrics.GetTodayProviderMetricsAsync(request.ProviderId, cancellationToken);
            if (metric is null)
                return ApiResponseDto<AmbulanceDashboardResponseDto>.Fail("আজকের Ambulance ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<AmbulanceDashboardResponseDto>.Ok(mapper.Map<AmbulanceDashboardResponseDto>(metric));
        }
    }

}
