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
    public class DeleteDoctorLeaveHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDoctorLeaveCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorLeaveCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorLeaves.GetByIdAsync(request.LeaveId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Leave record not found.");

            await uow.DoctorLeaves.DeleteAsync(request.LeaveId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Leave deleted successfully.");
        }
    }

}
