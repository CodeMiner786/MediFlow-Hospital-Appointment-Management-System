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
    public sealed class GetEmergencyVisitsByFilterQueryHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<GetEmergencyVisitsByFilterQuery, ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>> Handle(
            GetEmergencyVisitsByFilterQuery request, CancellationToken ct)
        {
            var dto = request.Dto;

            // ── GetQueryable() দিয়ে IQueryable পাওয়া → DB তে filter ──
            var query = uow.EmergencyVisits.GetQueryable();

            // ── Dynamic filtering — null না হলেই filter apply ──
            if (dto.TriageLevel.HasValue)
                query = query.Where(v => v.TriageLevel == dto.TriageLevel.Value);

            if (dto.Status.HasValue)
                query = query.Where(v => v.Status == dto.Status.Value);

            if (dto.DateFrom.HasValue)
                query = query.Where(v => v.ArrivalTime >= dto.DateFrom.Value);

            if (dto.DateTo.HasValue)
                query = query.Where(v => v.ArrivalTime <= dto.DateTo.Value);

            query = query.OrderByDescending(v => v.ArrivalTime);

            // ── ToPagedAsync() → DB-level Skip/Take ──
            var pagedResponse = await uow.EmergencyVisits.ToPagedAsync(
                query, dto.PageNumber, dto.PageSize, ct);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Emergency.EmergencyVisit,
                EmergencyVisitResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>.Ok(result);
        }
    }

}
