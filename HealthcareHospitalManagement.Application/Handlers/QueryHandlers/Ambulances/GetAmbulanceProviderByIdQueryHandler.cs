using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Application.Queries.Ambulances;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Ambulances
{
    public sealed class GetAmbulanceProviderByIdQueryHandler(IAmbulanceUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAmbulanceProviderByIdQuery, ApiResponseDto<AmbulanceProviderResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceProviderResponseDto>> Handle(
            GetAmbulanceProviderByIdQuery request, CancellationToken ct)
        {
            var provider = await uow.AmbulanceProviderProfiles.GetByIdAsync(request.ProviderId, ct);
            if (provider is null)
                return ApiResponseDto<AmbulanceProviderResponseDto>.Fail(
                    $"ProviderId '{request.ProviderId}' পাওয়া যায়নি।");

            var result = mapper.Map<AmbulanceProviderResponseDto>(provider);
            return ApiResponseDto<AmbulanceProviderResponseDto>.Ok(result);
        }
    }

}
