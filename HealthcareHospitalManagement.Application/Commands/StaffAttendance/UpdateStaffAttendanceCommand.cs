using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.StaffAttendance
{
    public record UpdateStaffAttendanceCommand(Guid AttendanceId, UpdateStaffAttendanceDto Dto)
    : IRequest<ApiResponseDto<StaffAttendanceDto>>;

}
