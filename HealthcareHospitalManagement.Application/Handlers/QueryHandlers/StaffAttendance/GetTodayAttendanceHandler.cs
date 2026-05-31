using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
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
    public class GetTodayAttendanceHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetTodayAttendanceQuery, ApiResponseDto<StaffAttendanceDto>>
    {
        public async Task<ApiResponseDto<StaffAttendanceDto>> Handle(
            GetTodayAttendanceQuery request, CancellationToken ct)
        {
            var entity = await uow.StaffAttendances.GetTodayAttendanceAsync(request.StaffId, ct);
            if (entity is null)
                return ApiResponseDto<StaffAttendanceDto>.FailResponse(
                    "No attendance record found for today.");

            return ApiResponseDto<StaffAttendanceDto>.SuccessResponse(
                mapper.Map<StaffAttendanceDto>(entity));
        }
    }

}
