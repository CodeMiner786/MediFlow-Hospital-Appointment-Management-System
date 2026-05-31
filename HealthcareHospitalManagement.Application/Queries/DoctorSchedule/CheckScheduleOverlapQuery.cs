using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorSchedule
{
    public record CheckScheduleOverlapQuery(Guid DoctorId, DayOfWeek Day, TimeOnly Start, TimeOnly End)
    : IRequest<ApiResponseDto<bool>>;

}
