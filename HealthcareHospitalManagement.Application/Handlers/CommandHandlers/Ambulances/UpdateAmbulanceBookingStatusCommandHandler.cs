using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Ambulances;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Ambulances
{
    public sealed class UpdateAmbulanceBookingStatusCommandHandler(
    IAmbulanceUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<UpdateAmbulanceBookingStatusCommand, ApiResponseDto<AmbulanceBookingResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceBookingResponseDto>> Handle(
            UpdateAmbulanceBookingStatusCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① Booking খোঁজা ──
            var booking = await uow.AmbulanceBookings.GetByIdAsync(dto.BookingId, cancellationToken);
            if (booking is null)
                return ApiResponseDto<AmbulanceBookingResponseDto>.Fail(
                    $"BookingId '{dto.BookingId}' পাওয়া যায়নি।");

            // ── ② Status অনুযায়ী Timestamp সেট করা ──
            booking.Status = dto.Status;

            switch (dto.Status)
            {
                case AmbulanceBookingStatus.Dispatched:
                    booking.DispatchedAt = DateTime.UtcNow;
                    break;
                case AmbulanceBookingStatus.Arrived:
                    booking.ArrivedAt = DateTime.UtcNow;
                    break;
                case AmbulanceBookingStatus.Completed:
                    booking.CompletedAt = DateTime.UtcNow;
                    if (dto.Fare.HasValue) booking.Fare = dto.Fare;

                    // ── Trip শেষ হলে Vehicle → Available ──
                    var vehicle = await uow.AmbulanceVehicles.GetByIdAsync(booking.VehicleId, cancellationToken);
                    if (vehicle is not null)
                    {
                        vehicle.Status = AmbulanceStatus.Available;
                        await uow.AmbulanceVehicles.UpdateAsync(vehicle, cancellationToken);
                    }
                    break;
            }

            booking.UpdatedAt = DateTime.UtcNow;
            await uow.AmbulanceBookings.UpdateAsync(booking, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<AmbulanceBookingResponseDto>(booking);
            return ApiResponseDto<AmbulanceBookingResponseDto>.Ok(
                result, $"Booking Status '{dto.Status}' এ আপডেট হয়েছে।");
        }
    }

}
