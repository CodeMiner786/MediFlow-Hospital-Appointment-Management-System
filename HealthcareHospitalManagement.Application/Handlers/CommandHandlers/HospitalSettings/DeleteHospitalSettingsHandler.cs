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
    public class DeleteHospitalSettingsHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteHospitalSettingsCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteHospitalSettingsCommand request, CancellationToken ct)
        {
            var entity = await uow.HospitalSettings.GetByIdAsync(request.SettingsId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Hospital settings not found.");

            await uow.HospitalSettings.DeleteAsync(request.SettingsId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Hospital settings deleted successfully.");
        }
    }

}
