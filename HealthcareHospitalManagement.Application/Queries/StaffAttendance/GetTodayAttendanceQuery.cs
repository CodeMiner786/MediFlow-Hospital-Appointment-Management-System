using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.StaffAttendance
{
    public record GetTodayAttendanceQuery(Guid StaffId)
    : IRequest<ApiResponseDto<StaffAttendanceDto>>;

}
