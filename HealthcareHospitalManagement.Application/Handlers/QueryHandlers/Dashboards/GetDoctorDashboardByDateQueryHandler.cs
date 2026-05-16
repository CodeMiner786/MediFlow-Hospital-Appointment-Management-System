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
    public sealed class GetDoctorDashboardByDateQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetDoctorDashboardByDateQuery, ApiResponseDto<DoctorDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<DoctorDashboardResponseDto>> Handle(
            GetDoctorDashboardByDateQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.DoctorDashboardMetrics.GetByDoctorAndDateAsync(request.DoctorId, request.Date);
            if (metric is null)
                return ApiResponseDto<DoctorDashboardResponseDto>.Fail("নির্দিষ্ট তারিখের Doctor ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<DoctorDashboardResponseDto>.Ok(mapper.Map<DoctorDashboardResponseDto>(metric));
        }
    }

}
