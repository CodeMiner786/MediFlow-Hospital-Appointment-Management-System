using HealthcareHospitalManagement.Application.Commands.DoctorScheduleSlot;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorScheduleSlot
{
    public class MarkSlotAsBlockedHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<MarkSlotAsBlockedCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            MarkSlotAsBlockedCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorScheduleSlots.GetByIdAsync(request.SlotId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Slot not found.");

            if (entity.IsBooked)
                return ApiResponseDto<bool>.FailResponse("Cannot block an already booked slot.");

            await uow.DoctorScheduleSlots.MarkSlotAsBlockedAsync(request.SlotId, request.Reason, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Slot blocked successfully.");
        }
    }

}
