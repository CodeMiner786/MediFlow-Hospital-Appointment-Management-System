using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.StaffAttendance;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.StaffAttendance
{
    public class UpdateStaffAttendanceHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateStaffAttendanceCommand, ApiResponseDto<StaffAttendanceDto>>
    {
        public async Task<ApiResponseDto<StaffAttendanceDto>> Handle(
            UpdateStaffAttendanceCommand request, CancellationToken ct)
        {
            var entity = await uow.StaffAttendances.GetByIdAsync(request.AttendanceId, ct);
            if (entity is null)
                return ApiResponseDto<StaffAttendanceDto>.FailResponse("Attendance record not found.");

            mapper.Map(request.Dto, entity);

            await uow.StaffAttendances.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<StaffAttendanceDto>.SuccessResponse(
                mapper.Map<StaffAttendanceDto>(entity), "Attendance updated successfully.");
        }
    }

}
