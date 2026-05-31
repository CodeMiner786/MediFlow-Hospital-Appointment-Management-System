using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorScheduleSlot
{
    public record UpdateDoctorScheduleSlotCommand(Guid SlotId, UpdateDoctorScheduleSlotDto Dto)
    : IRequest<ApiResponseDto<DoctorScheduleSlotDto>>;

}
