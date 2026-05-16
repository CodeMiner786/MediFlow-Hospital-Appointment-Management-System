using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Application.Queries.Ambulances;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Ambulances
{
    public sealed class GetActiveBookingsQueryHandler(IAmbulanceUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetActiveBookingsQuery, ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>> Handle(
            GetActiveBookingsQuery request, CancellationToken ct)
        {
            // ── Repository এর IAsyncEnumerable stream consume করা ──
            // GetActiveBookingsStream() → Dispatched, OnTheWay, Arrived status গুলো
            var allData = new List<AmbulanceBooking>();

            await foreach (var booking in uow.AmbulanceBookings.GetActiveBookingsStream(ct))
            {
                allData.Add(booking);
            }

            var totalRecords = allData.Count;

            var pagedResponse = PagedResponse<AmbulanceBooking>.Create(
                data: allData.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<AmbulanceBooking, AmbulanceBookingResponseDto>(mapper);
            return ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>.Ok(result);
        }
    }

}
