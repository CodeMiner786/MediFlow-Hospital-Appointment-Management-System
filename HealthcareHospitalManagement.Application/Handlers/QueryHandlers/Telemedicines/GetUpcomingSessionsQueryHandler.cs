using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Application.Queries.Telemedicines;
using HealthcareHospitalManagement.Domain.Enums.Telemedicine;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Telemedicines
{
    public sealed class GetUpcomingSessionsQueryHandler(ITelemedicineUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetUpcomingSessionsQuery, ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>> Handle(
            GetUpcomingSessionsQuery request, CancellationToken ct)
        {
            // ── Scheduled এবং ভবিষ্যতে আছে এমন session ──
            var now = DateTime.UtcNow;
            var query = uow.TelemedicineSessions
                .GetQueryable()
                .Where(s => s.Status == TelemedicineSessionStatus.Scheduled
                         && s.ScheduledAt >= now)
                .OrderBy(s => s.ScheduledAt);

            var pagedResponse = await uow.TelemedicineSessions
                .ToPagedAsync(query, request.PageNumber, request.PageSize, ct);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Telemedicine.TelemedicineSession,
                TelemedicineSessionResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>.Ok(result);
        }
    }

}
