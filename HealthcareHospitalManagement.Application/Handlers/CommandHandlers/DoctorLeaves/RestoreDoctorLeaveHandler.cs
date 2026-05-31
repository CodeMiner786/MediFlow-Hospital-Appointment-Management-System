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
    public class RestoreDoctorLeaveHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreDoctorLeaveCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreDoctorLeaveCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorLeaves.GetByIdAsync(request.LeaveId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Leave record not found.");

            await uow.DoctorLeaves.RestoreAsync(request.LeaveId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Leave restored successfully.");
        }
    }

}
