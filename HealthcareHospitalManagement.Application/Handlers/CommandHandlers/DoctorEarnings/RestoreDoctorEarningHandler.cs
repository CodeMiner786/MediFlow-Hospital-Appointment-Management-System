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
    public class RestoreDoctorEarningHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreDoctorEarningCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreDoctorEarningCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorEarnings.GetByIdAsync(request.EarningId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Earning record not found.");

            await uow.DoctorEarnings.RestoreAsync(request.EarningId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Earning restored successfully.");
        }
    }

}
