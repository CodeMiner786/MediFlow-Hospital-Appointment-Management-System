using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
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
    public class GetSlotByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetSlotByIdQuery, ApiResponseDto<DoctorScheduleSlotDto>>
    {
        public async Task<ApiResponseDto<DoctorScheduleSlotDto>> Handle(
            GetSlotByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorScheduleSlots.GetByIdAsync(request.SlotId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorScheduleSlotDto>.FailResponse("Slot not found.");

            return ApiResponseDto<DoctorScheduleSlotDto>.SuccessResponse(
                mapper.Map<DoctorScheduleSlotDto>(entity));
        }
    }

}
