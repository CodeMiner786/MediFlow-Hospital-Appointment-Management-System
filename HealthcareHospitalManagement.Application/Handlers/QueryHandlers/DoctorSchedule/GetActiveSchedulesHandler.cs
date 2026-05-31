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
    public class GetActiveSchedulesHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetActiveSchedulesQuery, ApiResponseDto<PagedResultDto<DoctorScheduleDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorScheduleDto>>> Handle(
            GetActiveSchedulesQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorSchedules.GetActiveSchedulesStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorSchedule, DoctorScheduleDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorScheduleDto>>.SuccessResponse(paged);
        }
    }

}
