using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using HealthcareHospitalManagement.Application.Queries.Emergencys;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Emergencys
{
    public sealed class GetEmergencyVisitByIdQueryHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<GetEmergencyVisitByIdQuery, ApiResponseDto<EmergencyVisitResponseDto>>
    {
        public async Task<ApiResponseDto<EmergencyVisitResponseDto>> Handle(
            GetEmergencyVisitByIdQuery request, CancellationToken ct)
        {
            var visit = await uow.EmergencyVisits.GetByIdAsync(request.EmergencyVisitId, ct);
            if (visit is null)
                return ApiResponseDto<EmergencyVisitResponseDto>.Fail(
                    $"EmergencyVisitId '{request.EmergencyVisitId}' পাওয়া যায়নি।");

            var result = mapper.Map<EmergencyVisitResponseDto>(visit);
            return ApiResponseDto<EmergencyVisitResponseDto>.Ok(result);
        }
    }

}
