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
    public sealed class GetPharmacyDashboardByDateQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetPharmacyDashboardByDateQuery, ApiResponseDto<PharmacyDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<PharmacyDashboardResponseDto>> Handle(
            GetPharmacyDashboardByDateQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.PharmacyDashboardMetrics.GetByPharmacyAndDateAsync(request.PharmacyProfileId, request.Date);
            if (metric is null)
                return ApiResponseDto<PharmacyDashboardResponseDto>.Fail("নির্দিষ্ট তারিখের Pharmacy ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<PharmacyDashboardResponseDto>.Ok(mapper.Map<PharmacyDashboardResponseDto>(metric));
        }
    }

}
