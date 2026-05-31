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
    public class SoftDeleteDoctorScheduleSlotHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteDoctorScheduleSlotCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteDoctorScheduleSlotCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorScheduleSlots.GetByIdAsync(request.SlotId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Slot not found.");

            await uow.DoctorScheduleSlots.SoftDeleteAsync(request.SlotId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Slot soft deleted successfully.");
        }
    }

}
