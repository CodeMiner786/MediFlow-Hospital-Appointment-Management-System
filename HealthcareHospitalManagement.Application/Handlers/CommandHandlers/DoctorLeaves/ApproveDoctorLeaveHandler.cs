using HealthcareHospitalManagement.Application.Commands.DoctorLeaves;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorLeaves
{
    public class ApproveDoctorLeaveHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<ApproveDoctorLeaveCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            ApproveDoctorLeaveCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorLeaves.GetByIdAsync(request.LeaveId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Leave record not found.");

            await uow.DoctorLeaves.UpdateLeaveStatusAsync(
                request.LeaveId,
                request.Dto.IsApproved,
                request.Dto.ApprovedBy,
                request.Dto.RejectionReason,
                ct);

            await uow.SaveChangesAsync(ct);

            var msg = request.Dto.IsApproved ? "Leave approved successfully." : "Leave rejected.";
            return ApiResponseDto<bool>.SuccessResponse(true, msg);
        }
    }

}
