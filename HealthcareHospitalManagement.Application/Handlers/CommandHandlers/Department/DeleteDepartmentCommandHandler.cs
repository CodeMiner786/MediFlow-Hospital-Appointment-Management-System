using HealthcareHospitalManagement.Application.Commands.Department;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Department
{
    public sealed class DeleteDepartmentCommandHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDepartmentCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await uow.Departments.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Department not found.");

            await uow.Departments.DeleteAsync(entity.Id, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.SuccessResponse(true, "Department deleted successfully.");
        }
    }

}
