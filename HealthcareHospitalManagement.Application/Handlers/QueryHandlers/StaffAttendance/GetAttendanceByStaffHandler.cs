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
    public class GetAttendanceByStaffHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAttendanceByStaffQuery, ApiResponseDto<PagedResultDto<StaffAttendanceDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<StaffAttendanceDto>>> Handle(
            GetAttendanceByStaffQuery request, CancellationToken ct)
        {
            var stream = uow.StaffAttendances.GetAttendanceByStaffStream(request.StaffId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.StaffAttendance, StaffAttendanceDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<StaffAttendanceDto>>.SuccessResponse(paged);
        }
    }

}
