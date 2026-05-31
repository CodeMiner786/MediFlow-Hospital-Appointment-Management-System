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
    public class CheckInHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<CheckInCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            CheckInCommand request, CancellationToken ct)
        {
            await uow.StaffAttendances.UpdateCheckInAsync(
                request.StaffId, request.Date, request.CheckInTime, ct);

            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Check-in recorded successfully.");
        }
    }

}
