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
    public class GetSlotsByDoctorAndDateHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetSlotsByDoctorAndDateQuery, ApiResponseDto<PagedResultDto<DoctorScheduleSlotDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorScheduleSlotDto>>> Handle(
            GetSlotsByDoctorAndDateQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorScheduleSlots.GetSlotsByDoctorAndDateStream(request.DoctorId, request.Date);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorScheduleSlot, DoctorScheduleSlotDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorScheduleSlotDto>>.SuccessResponse(paged);
        }
    }

}
