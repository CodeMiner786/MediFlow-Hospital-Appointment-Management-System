using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorPerformanceReport;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using HealthcareHospitalManagement.Domain.Enums.Report;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorPerformanceReport
{
    public class CreateDoctorPerformanceReportHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorPerformanceReportCommand, ApiResponseDto<DoctorPerformanceReportDto>>
    {
        public async Task<ApiResponseDto<DoctorPerformanceReportDto>> Handle(
            CreateDoctorPerformanceReportCommand request, CancellationToken ct)
        {
            var existing = await uow.DoctorPerformanceReports.GetReportByDateRangeAsync(
                request.Dto.DoctorId, request.Dto.FromDate, request.Dto.ToDate);

            if (existing is not null)
                return ApiResponseDto<DoctorPerformanceReportDto>.FailResponse(
                    "A report already exists for this doctor in the given date range.");

            var entity = mapper.Map<Domain.Entities.Doctor.DoctorPerformanceReport>(request.Dto);
            entity.GeneratedAt = DateTime.UtcNow;
            entity.Status = ReportStatus.Draft;

            await uow.DoctorPerformanceReports.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorPerformanceReportDto>.SuccessResponse(
                mapper.Map<DoctorPerformanceReportDto>(entity), "Performance report created successfully.");
        }
    }

}
