
using HealthcareHospitalManagement.Application.Commands.HospitalSettings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.HospitalSettings
{
    public class SoftDeleteHospitalSettingsHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteHospitalSettingsCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteHospitalSettingsCommand request, CancellationToken ct)
        {
            var entity = await uow.HospitalSettings.GetByIdAsync(request.SettingsId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Hospital settings not found.");

            await uow.HospitalSettings.SoftDeleteAsync(request.SettingsId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Hospital settings soft deleted successfully.");
        }
    }

}
