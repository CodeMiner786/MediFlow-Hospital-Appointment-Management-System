using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorScheduleSlot
{
    public record SoftDeleteDoctorScheduleSlotCommand(Guid SlotId)
    : IRequest<ApiResponseDto<bool>>;

}
