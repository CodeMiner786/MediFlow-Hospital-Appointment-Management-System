using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorSchedule
{
    public record GetScheduleWithSlotsQuery(Guid ScheduleId)
    : IRequest<ApiResponseDto<DoctorScheduleDto>>;

}
