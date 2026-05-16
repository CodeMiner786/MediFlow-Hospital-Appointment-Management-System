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
    public sealed class GetPatientDashboardQueryHandler(
    IDashboardUnitOfWork unitOfWork
) : IRequestHandler<GetPatientDashboardQuery, ApiResponseDto<PatientDashboardResponseDto>>
    {
        public Task<ApiResponseDto<PatientDashboardResponseDto>> Handle(
            GetPatientDashboardQuery request,
            CancellationToken cancellationToken)
        {
            // Patient dashboard is computed on-the-fly from multiple aggregates.
            // Extend this handler with the required unit-of-works when ready.
            _ = unitOfWork; // suppress unused warning until implementation is added
            var dto = new PatientDashboardResponseDto();
            return Task.FromResult(ApiResponseDto<PatientDashboardResponseDto>.Ok(dto, "Patient ড্যাশবোর্ড ডেটা সফলভাবে পাওয়া গেছে।"));
        }
    }

}
