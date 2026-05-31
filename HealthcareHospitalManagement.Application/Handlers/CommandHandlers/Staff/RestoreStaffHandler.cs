using HealthcareHospitalManagement.Application.Commands.Staff;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Staff
{
    public class RestoreStaffHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreStaffCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreStaffCommand request, CancellationToken ct)
        {
            var entity = await uow.Staff.GetByIdAsync(request.StaffId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Staff member not found.");

            await uow.Staff.RestoreAsync(request.StaffId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Staff restored successfully.");
        }
    }

}
