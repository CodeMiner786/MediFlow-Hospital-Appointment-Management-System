using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetSlotsByScheduleIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetSlotsByScheduleIdQuery, ApiResponseDto<PagedResultDto<DoctorScheduleSlotDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorScheduleSlotDto>>> Handle(
            GetSlotsByScheduleIdQuery request, CancellationToken ct)
        {
            var entities = await uow.DoctorScheduleSlots.GetSlotsByScheduleIdAsync(request.ScheduleId, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorScheduleSlot, DoctorScheduleSlotDto>(
                               [.. entities], request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorScheduleSlotDto>>.SuccessResponse(paged);
        }
    }

}
