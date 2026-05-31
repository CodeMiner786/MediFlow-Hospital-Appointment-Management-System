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
    public class CreateDoctorScheduleSlotHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorScheduleSlotCommand, ApiResponseDto<DoctorScheduleSlotDto>>
    {
        public async Task<ApiResponseDto<DoctorScheduleSlotDto>> Handle(
            CreateDoctorScheduleSlotCommand request, CancellationToken ct)
        {
            var entity = mapper.Map<Domain.Entities.Doctor.DoctorScheduleSlot>(request.Dto);

            await uow.DoctorScheduleSlots.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorScheduleSlotDto>.SuccessResponse(
                mapper.Map<DoctorScheduleSlotDto>(entity), "Schedule slot created successfully.");
        }
    }

}
