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
    public sealed class GetAllAmbulanceBookingsQueryHandler(IAmbulanceUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAllAmbulanceBookingsQuery, ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>> Handle(
            GetAllAmbulanceBookingsQuery request, CancellationToken ct)
        {
            // ── DB-level pagination: GetQueryable() + ToPagedAsync() ──
            var query = uow.AmbulanceBookings
                .GetQueryable()
                .OrderByDescending(b => b.RequestedAt);

            var pagedResponse = await uow.AmbulanceBookings
                .ToPagedAsync(query, request.PageNumber, request.PageSize, ct);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Ambulance.AmbulanceBooking,
                AmbulanceBookingResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>.Ok(result);
        }
    }

}
