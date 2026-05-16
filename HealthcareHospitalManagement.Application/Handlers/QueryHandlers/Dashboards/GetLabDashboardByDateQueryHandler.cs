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
    public sealed class GetLabDashboardByDateQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetLabDashboardByDateQuery, ApiResponseDto<LabDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<LabDashboardResponseDto>> Handle(
            GetLabDashboardByDateQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.LabDashboardMetrics.GetByLabAndDateAsync(request.LabProfileId, request.Date);
            if (metric is null)
                return ApiResponseDto<LabDashboardResponseDto>.Fail("নির্দিষ্ট তারিখের Lab ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<LabDashboardResponseDto>.Ok(mapper.Map<LabDashboardResponseDto>(metric));
        }
    }
}
