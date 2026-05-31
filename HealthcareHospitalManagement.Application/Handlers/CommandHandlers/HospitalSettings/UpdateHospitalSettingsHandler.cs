using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.HospitalSettings;
using HealthcareHospitalManagement.Application.DTOs.HospitalSettings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.HospitalSettings
{
    public class UpdateHospitalSettingsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateHospitalSettingsCommand, ApiResponseDto<HospitalSettingsDto>>
    {
        public async Task<ApiResponseDto<HospitalSettingsDto>> Handle(
            UpdateHospitalSettingsCommand request, CancellationToken ct)
        {
            var entity = await uow.HospitalSettings.GetByIdAsync(request.SettingsId, ct);
            if (entity is null)
                return ApiResponseDto<HospitalSettingsDto>.FailResponse("Hospital settings not found.");

            if (request.Dto.DefaultPlatformSharePercent + request.Dto.DefaultDoctorSharePercent != 100)
                return ApiResponseDto<HospitalSettingsDto>.FailResponse(
                    "Platform and doctor share percentages must sum to 100.");

            mapper.Map(request.Dto, entity);

            await uow.HospitalSettings.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<HospitalSettingsDto>.SuccessResponse(
                mapper.Map<HospitalSettingsDto>(entity), "Hospital settings updated successfully.");
        }
    }

}
