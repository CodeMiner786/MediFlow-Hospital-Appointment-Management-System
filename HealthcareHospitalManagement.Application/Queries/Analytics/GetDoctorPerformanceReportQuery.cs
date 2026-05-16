using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Analytics
{
    public record GetDoctorPerformanceReportQuery(ReportFilterRequestDto RequestDto)
    : IRequest<ApiResponse<List<DoctorPerformanceResponseDto>>>;

}
