using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorPerformanceReport
{
    public record UpdateDoctorPerformanceReportCommand(Guid ReportId, UpdateDoctorPerformanceReportDto Dto)
    : IRequest<ApiResponseDto<DoctorPerformanceReportDto>>;

}
