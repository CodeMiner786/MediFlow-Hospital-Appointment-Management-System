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
    public class DeleteStaffHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteStaffCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteStaffCommand request, CancellationToken ct)
        {
            var entity = await uow.Staff.GetByIdAsync(request.StaffId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Staff member not found.");

            await uow.Staff.DeleteAsync(request.StaffId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Staff deleted successfully.");
        }
    }

}
