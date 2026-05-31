using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using HealthcareHospitalManagement.Application.Helpers.Stream;
using HealthcareHospitalManagement.Application.Queries.StaffAttendance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.StaffAttendance
{
    public class GetDailyAttendanceHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDailyAttendanceQuery, ApiResponseDto<PagedResultDto<StaffAttendanceDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<StaffAttendanceDto>>> Handle(
            GetDailyAttendanceQuery request, CancellationToken ct)
        {
            var stream = uow.StaffAttendances.GetDailyAttendanceStream(request.Date);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.StaffAttendance, StaffAttendanceDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<StaffAttendanceDto>>.SuccessResponse(paged);
        }
    }

}
