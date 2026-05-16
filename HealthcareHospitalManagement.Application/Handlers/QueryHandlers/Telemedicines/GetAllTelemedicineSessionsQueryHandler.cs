using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Application.Queries.Telemedicines;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Telemedicines
{
    public sealed class GetAllTelemedicineSessionsQueryHandler(ITelemedicineUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAllTelemedicineSessionsQuery, ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>> Handle(
            GetAllTelemedicineSessionsQuery request, CancellationToken ct)
        {
            // ── DB-level pagination: GetQueryable() + ToPagedAsync() ──
            var query = uow.TelemedicineSessions
                .GetQueryable()
                .OrderByDescending(s => s.ScheduledAt);

            var pagedResponse = await uow.TelemedicineSessions
                .ToPagedAsync(query, request.PageNumber, request.PageSize, ct);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Telemedicine.TelemedicineSession,
                TelemedicineSessionResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>.Ok(result);
        }
    }

}
