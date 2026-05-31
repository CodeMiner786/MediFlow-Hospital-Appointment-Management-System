using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorScheduleSlot
{
    public record GetSlotByIdQuery(Guid SlotId)
    : IRequest<ApiResponseDto<DoctorScheduleSlotDto>>;

}
