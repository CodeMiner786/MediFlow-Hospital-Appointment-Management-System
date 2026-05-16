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
    public sealed class GetSessionsByPatientQueryHandler(ITelemedicineUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetSessionsByPatientQuery, ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>> Handle(
            GetSessionsByPatientQuery request, CancellationToken ct)
        {
            var query = uow.TelemedicineSessions
                .GetQueryable()
                .Where(s => s.PatientId == request.PatientId)
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
