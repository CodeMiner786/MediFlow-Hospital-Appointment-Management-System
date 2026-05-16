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
    public sealed class GetAllEmergencyVisitsQueryHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<GetAllEmergencyVisitsQuery, ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>> Handle(
            GetAllEmergencyVisitsQuery request, CancellationToken ct)
        {
            // ── GetQueryable() → DB-level pagination → ToPagedAsync() ──
            // In-memory এ সব লোড না করে সরাসরি DB তে Skip/Take করে
            var query = uow.EmergencyVisits
                .GetQueryable()
                .OrderByDescending(v => v.ArrivalTime);

            var pagedResponse = await uow.EmergencyVisits.ToPagedAsync(
                query, request.PageNumber, request.PageSize, ct);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Emergency.EmergencyVisit,
                EmergencyVisitResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>.Ok(result);
        }
    }

}
