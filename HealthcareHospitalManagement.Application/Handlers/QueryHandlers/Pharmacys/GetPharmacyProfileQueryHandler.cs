using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Pharmacys
{
    public sealed class GetPharmacyProfileQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPharmacyProfileQuery, ApiResponseDto<PharmacyProfileResponseDto>>
    {
        public async Task<ApiResponseDto<PharmacyProfileResponseDto>> Handle(
            GetPharmacyProfileQuery request, CancellationToken ct)
        {
            var profile = await uow.PharmacyProfiles.GetByIdAsync(request.PharmacyId, ct);
            if (profile is null)
                return ApiResponseDto<PharmacyProfileResponseDto>.Fail(
                    $"PharmacyId '{request.PharmacyId}' পাওয়া যায়নি।");

            var result = mapper.Map<PharmacyProfileResponseDto>(profile);
            return ApiResponseDto<PharmacyProfileResponseDto>.Ok(result);
        }
    }

}
