using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.StaffAttendance
{
    public record DeleteStaffAttendanceCommand(Guid AttendanceId)
    : IRequest<ApiResponseDto<bool>>;

}
