using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorPerformanceReport
{
    public record RestoreDoctorPerformanceReportCommand(Guid ReportId)
    : IRequest<ApiResponseDto<bool>>;

}
