using HealthcareHospitalManagement.Application.Commands.DoctorPerformanceReport;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorPerformanceReport
{
    public class RestoreDoctorPerformanceReportHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreDoctorPerformanceReportCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreDoctorPerformanceReportCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorPerformanceReports.GetByIdAsync(request.ReportId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Report not found.");

            await uow.DoctorPerformanceReports.RestoreAsync(request.ReportId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Report restored successfully.");
        }
    }

}
