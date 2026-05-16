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
    public sealed class GetAdminDashboardByDateQueryHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<GetAdminDashboardByDateQuery, ApiResponseDto<AdminDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<AdminDashboardResponseDto>> Handle(
            GetAdminDashboardByDateQuery request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.AdminDashboardMetrics.GetByDateAsync(request.Date);
            if (metric is null)
                return ApiResponseDto<AdminDashboardResponseDto>.Fail($"{request.Date} তারিখের Admin ড্যাশবোর্ড ডেটা পাওয়া যায়নি।");

            return ApiResponseDto<AdminDashboardResponseDto>.Ok(mapper.Map<AdminDashboardResponseDto>(metric));
        }
    }

}
