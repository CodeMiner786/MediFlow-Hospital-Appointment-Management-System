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
    public class UpdateDoctorScheduleHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorScheduleCommand, ApiResponseDto<DoctorScheduleDto>>
    {
        public async Task<ApiResponseDto<DoctorScheduleDto>> Handle(
            UpdateDoctorScheduleCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorSchedules.GetByIdAsync(request.ScheduleId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorScheduleDto>.FailResponse("Schedule not found.");

            // অন্য শিডিউলের সাথে ওভারল্যাপ চেক (নিজেকে বাদ দিয়ে)
            var hasOverlap = await uow.DoctorSchedules.HasScheduleOverlapAsync(
                entity.DoctorId,
                request.Dto.DayOfWeek,
                request.Dto.StartTime,
                request.Dto.EndTime);

            if (hasOverlap && (entity.DayOfWeek != request.Dto.DayOfWeek ||
                entity.StartTime != request.Dto.StartTime ||
                entity.EndTime != request.Dto.EndTime))
                return ApiResponseDto<DoctorScheduleDto>.FailResponse(
                    "Updated schedule overlaps with an existing schedule.");

            mapper.Map(request.Dto, entity);

            await uow.DoctorSchedules.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorScheduleDto>.SuccessResponse(
                mapper.Map<DoctorScheduleDto>(entity), "Schedule updated successfully.");
        }
    }

}
