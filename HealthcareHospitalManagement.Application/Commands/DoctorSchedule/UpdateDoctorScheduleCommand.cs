using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorSchedule
{
    public record UpdateDoctorScheduleCommand(Guid ScheduleId, UpdateDoctorScheduleDto Dto)
    : IRequest<ApiResponseDto<DoctorScheduleDto>>;

}
