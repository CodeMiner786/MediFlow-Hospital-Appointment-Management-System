using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.HospitalSettings;
using HealthcareHospitalManagement.Application.Queries.HospitalSettings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.HospitalSettings
{
    public class GetCurrentSettingsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetCurrentSettingsQuery, ApiResponseDto<HospitalSettingsDto>>
    {
        public async Task<ApiResponseDto<HospitalSettingsDto>> Handle(
            GetCurrentSettingsQuery request, CancellationToken ct)
        {
            var entity = await uow.HospitalSettings.GetCurrentSettingsAsync(ct);
            if (entity is null)
                return ApiResponseDto<HospitalSettingsDto>.FailResponse("No hospital settings found.");

            return ApiResponseDto<HospitalSettingsDto>.SuccessResponse(
                mapper.Map<HospitalSettingsDto>(entity));
        }
    }

}
