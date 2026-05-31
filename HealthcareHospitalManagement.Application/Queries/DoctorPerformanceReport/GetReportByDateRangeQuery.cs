using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorPerformanceReport
{
    public record GetReportByDateRangeQuery(Guid DoctorId, DateTime FromDate, DateTime ToDate)
    : IRequest<ApiResponseDto<DoctorPerformanceReportDto>>;

}
