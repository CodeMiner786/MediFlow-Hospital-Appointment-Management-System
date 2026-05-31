using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorScheduleSlot
{
    public record MarkSlotAsBookedCommand(Guid SlotId, Guid AppointmentId)
    : IRequest<ApiResponseDto<bool>>;

}
