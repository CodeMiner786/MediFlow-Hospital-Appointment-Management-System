using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.StaffAttendance
{
    public record CheckInCommand(Guid StaffId, DateOnly Date, TimeOnly CheckInTime)
    : IRequest<ApiResponseDto<bool>>;

}
