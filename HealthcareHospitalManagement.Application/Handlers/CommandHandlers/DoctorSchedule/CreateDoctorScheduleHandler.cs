using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorSchedule;
using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorSchedule
{
    public class CreateDoctorScheduleHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorScheduleCommand, ApiResponseDto<DoctorScheduleDto>>
    {
        public async Task<ApiResponseDto<DoctorScheduleDto>> Handle(
            CreateDoctorScheduleCommand request, CancellationToken ct)
        {
            var hasOverlap = await uow.DoctorSchedules.HasScheduleOverlapAsync(
                request.Dto.DoctorId,
                request.Dto.DayOfWeek,
                request.Dto.StartTime,
                request.Dto.EndTime);

            if (hasOverlap)
                return ApiResponseDto<DoctorScheduleDto>.FailResponse(
                    "Schedule overlaps with an existing schedule for this doctor.");

            var entity = mapper.Map<Domain.Entities.Doctor.DoctorSchedule>(request.Dto);

            await uow.DoctorSchedules.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorScheduleDto>.SuccessResponse(
                mapper.Map<DoctorScheduleDto>(entity), "Schedule created successfully.");
        }
    }

}
