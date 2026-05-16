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
    public sealed class GetAmbulanceBookingByIdQueryHandler(IAmbulanceUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAmbulanceBookingByIdQuery, ApiResponseDto<AmbulanceBookingResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceBookingResponseDto>> Handle(
            GetAmbulanceBookingByIdQuery request, CancellationToken ct)
        {
            var booking = await uow.AmbulanceBookings.GetByIdAsync(request.BookingId, ct);
            if (booking is null)
                return ApiResponseDto<AmbulanceBookingResponseDto>.Fail(
                    $"BookingId '{request.BookingId}' পাওয়া যায়নি।");

            var result = mapper.Map<AmbulanceBookingResponseDto>(booking);
            return ApiResponseDto<AmbulanceBookingResponseDto>.Ok(result);
        }
    }

}
