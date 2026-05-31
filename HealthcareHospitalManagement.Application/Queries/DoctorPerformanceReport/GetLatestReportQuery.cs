using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorPerformanceReport
{
    public record GetLatestReportQuery(Guid DoctorId)
    : IRequest<ApiResponseDto<DoctorPerformanceReportDto>>;

}
