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
    public class GetAttendanceByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAttendanceByIdQuery, ApiResponseDto<StaffAttendanceDto>>
    {
        public async Task<ApiResponseDto<StaffAttendanceDto>> Handle(
            GetAttendanceByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.StaffAttendances.GetByIdAsync(request.AttendanceId, ct);
            if (entity is null)
                return ApiResponseDto<StaffAttendanceDto>.FailResponse("Attendance record not found.");

            return ApiResponseDto<StaffAttendanceDto>.SuccessResponse(
                mapper.Map<StaffAttendanceDto>(entity));
        }
    }

}
