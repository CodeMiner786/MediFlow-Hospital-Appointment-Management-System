using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorScheduleSlot;
using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorScheduleSlot
{
    public class UpdateDoctorScheduleSlotHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorScheduleSlotCommand, ApiResponseDto<DoctorScheduleSlotDto>>
    {
        public async Task<ApiResponseDto<DoctorScheduleSlotDto>> Handle(
            UpdateDoctorScheduleSlotCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorScheduleSlots.GetByIdAsync(request.SlotId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorScheduleSlotDto>.FailResponse("Slot not found.");

            if (entity.IsBooked)
                return ApiResponseDto<DoctorScheduleSlotDto>.FailResponse("Booked slot cannot be modified.");

            mapper.Map(request.Dto, entity);

            await uow.DoctorScheduleSlots.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorScheduleSlotDto>.SuccessResponse(
                mapper.Map<DoctorScheduleSlotDto>(entity), "Slot updated successfully.");
        }
    }

}
