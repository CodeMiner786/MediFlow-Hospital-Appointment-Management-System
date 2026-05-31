using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorScheduleSlot
{
    public record IsSlotAvailableQuery(Guid SlotId)
    : IRequest<ApiResponseDto<bool>>;

}
