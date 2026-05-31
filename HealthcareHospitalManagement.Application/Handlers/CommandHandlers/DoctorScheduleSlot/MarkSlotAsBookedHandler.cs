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
    public class MarkSlotAsBookedHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<MarkSlotAsBookedCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            MarkSlotAsBookedCommand request, CancellationToken ct)
        {
            var isAvailable = await uow.DoctorScheduleSlots.IsSlotAvailableAsync(request.SlotId,ct);
            if (!isAvailable)
                return ApiResponseDto<bool>.FailResponse("Slot is not available for booking.");

            var success = await uow.DoctorScheduleSlots.MarkSlotAsBookedAsync(
                request.SlotId, request.AppointmentId,ct);

            if (!success)
                return ApiResponseDto<bool>.FailResponse("Failed to book the slot.");

            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Slot marked as booked successfully.");
        }
    }

}
