using HealthcareHospitalManagement.Application.Commands.DoctorEarnings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorEarnings
{
    public class DeleteDoctorEarningHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDoctorEarningCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorEarningCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorEarnings.GetByIdAsync(request.EarningId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Earning record not found.");

            await uow.DoctorEarnings.SoftDeleteAsync(request.EarningId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Earning deleted successfully.");
        }
    }

}
