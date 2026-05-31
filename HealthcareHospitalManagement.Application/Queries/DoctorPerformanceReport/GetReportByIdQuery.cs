using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorPerformanceReport
{
    public record GetReportByIdQuery(Guid ReportId)
    : IRequest<ApiResponseDto<DoctorPerformanceReportDto>>;

}
