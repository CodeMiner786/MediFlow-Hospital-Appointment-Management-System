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
    public class UpdateDefaultSharesHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<UpdateDefaultSharesCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            UpdateDefaultSharesCommand request, CancellationToken ct)
        {
            if (request.PlatformShare + request.DoctorShare != 100)
                return ApiResponseDto<bool>.FailResponse(
                    "Platform and doctor share percentages must sum to 100.");

            var existing = await uow.HospitalSettings.GetCurrentSettingsAsync(ct);
            if (existing is null)
                return ApiResponseDto<bool>.FailResponse("Hospital settings not found.");

            await uow.HospitalSettings.UpdateDefaultSharesAsync(
                request.PlatformShare, request.DoctorShare, ct);

            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Default shares updated successfully.");
        }
    }

}
