using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetSchedulesByDayHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetSchedulesByDayQuery, ApiResponseDto<PagedResultDto<DoctorScheduleDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorScheduleDto>>> Handle(
            GetSchedulesByDayQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorSchedules.GetSchedulesByDayStream(request.DoctorId, request.Day);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorSchedule, DoctorScheduleDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorScheduleDto>>.SuccessResponse(paged);
        }
    }

}
