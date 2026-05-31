using HealthcareHospitalManagement.Application.Commands.StaffAttendance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.StaffAttendance
{
    public class SoftDeleteStaffAttendanceHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteStaffAttendanceCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteStaffAttendanceCommand request, CancellationToken ct)
        {
            var entity = await uow.StaffAttendances.GetByIdAsync(request.AttendanceId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Attendance record not found.");

            await uow.StaffAttendances.SoftDeleteAsync(request.AttendanceId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Attendance soft deleted successfully.");
        }
    }

}
