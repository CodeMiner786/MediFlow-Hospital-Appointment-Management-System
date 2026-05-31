using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorPerformanceReport;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorPerformanceReport
{
    public class UpdateDoctorPerformanceReportHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorPerformanceReportCommand, ApiResponseDto<DoctorPerformanceReportDto>>
    {
        public async Task<ApiResponseDto<DoctorPerformanceReportDto>> Handle(
            UpdateDoctorPerformanceReportCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorPerformanceReports.GetByIdAsync(request.ReportId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorPerformanceReportDto>.FailResponse("Report not found.");

            mapper.Map(request.Dto, entity);

            await uow.DoctorPerformanceReports.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorPerformanceReportDto>.SuccessResponse(
                mapper.Map<DoctorPerformanceReportDto>(entity), "Report updated successfully.");
        }
    }

}
