using HealthcareHospitalManagement.Application.Queries.DoctorScheduleSlot;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorScheduleSlot
{
    public class IsSlotAvailableHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<IsSlotAvailableQuery, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            IsSlotAvailableQuery request, CancellationToken ct)
        {
            var isAvailable = await uow.DoctorScheduleSlots.IsSlotAvailableAsync(request.SlotId, ct);
            return ApiResponseDto<bool>.SuccessResponse(isAvailable,
                isAvailable ? "Slot is available." : "Slot is not available.");
        }
    }

}
