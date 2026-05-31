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
    public class CheckOutHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<CheckOutCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            CheckOutCommand request, CancellationToken ct)
        {
            await uow.StaffAttendances.UpdateCheckOutAsync(
                request.StaffId, request.Date, request.CheckOutTime, ct);

            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Check-out recorded successfully.");
        }
    }

}
