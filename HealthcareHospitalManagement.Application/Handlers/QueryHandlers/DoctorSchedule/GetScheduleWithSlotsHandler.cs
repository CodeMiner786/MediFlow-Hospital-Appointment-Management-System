using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using HealthcareHospitalManagement.Application.Queries.DoctorSchedule;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorSchedule
{
    public class GetScheduleWithSlotsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetScheduleWithSlotsQuery, ApiResponseDto<DoctorScheduleDto>>
    {
        public async Task<ApiResponseDto<DoctorScheduleDto>> Handle(
            GetScheduleWithSlotsQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorSchedules.GetScheduleWithSlotsAsync(request.ScheduleId);
            if (entity is null)
                return ApiResponseDto<DoctorScheduleDto>.FailResponse("Schedule not found.");

            return ApiResponseDto<DoctorScheduleDto>.SuccessResponse(
                mapper.Map<DoctorScheduleDto>(entity));
        }
    }

}
