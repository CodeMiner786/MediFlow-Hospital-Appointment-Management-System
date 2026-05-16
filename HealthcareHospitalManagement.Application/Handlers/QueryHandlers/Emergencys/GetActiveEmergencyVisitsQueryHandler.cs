using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using HealthcareHospitalManagement.Application.Queries.Emergencys;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Emergencys
{
    public sealed class GetActiveEmergencyVisitsQueryHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<GetActiveEmergencyVisitsQuery, ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>> Handle(
            GetActiveEmergencyVisitsQuery request, CancellationToken ct)
        {
            // ── Active = Arrived বা UnderTreatment ──
            var query = uow.EmergencyVisits
                .GetQueryable()
                .Where(v => v.Status == EmergencyStatus.Arrived
                         || v.Status == EmergencyStatus.UnderTreatment)
                .OrderBy(v => v.TriageLevel)   // Critical আগে
                .ThenBy(v => v.ArrivalTime);   // আগে আসা আগে

            var pagedResponse = await uow.EmergencyVisits.ToPagedAsync(
                query, request.PageNumber, request.PageSize, ct);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Emergency.EmergencyVisit,
                EmergencyVisitResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>.Ok(result);
        }
    }

}
