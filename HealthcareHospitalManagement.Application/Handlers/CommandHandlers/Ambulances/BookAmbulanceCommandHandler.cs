using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Ambulances;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
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
    public sealed class BookAmbulanceCommandHandler(
    IAmbulanceUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<BookAmbulanceCommand, ApiResponseDto<AmbulanceBookingResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceBookingResponseDto>> Handle(
            BookAmbulanceCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① Vehicle আছে ও Available কিনা চেক ──
            var vehicle = await uow.AmbulanceVehicles.GetByIdAsync(dto.VehicleId, cancellationToken);
            if (vehicle is null)
                return ApiResponseDto<AmbulanceBookingResponseDto>.Fail(
                    $"VehicleId '{dto.VehicleId}' পাওয়া যায়নি।");

            if (vehicle.Status != AmbulanceStatus.Available)
                return ApiResponseDto<AmbulanceBookingResponseDto>.Fail(
                    $"এই Vehicle টি এখন Available নেই। বর্তমান Status: {vehicle.Status}");

            // ── ② Provider আছে কিনা চেক ──
            var provider = await uow.AmbulanceProviderProfiles.GetByIdAsync(dto.ProviderId, cancellationToken);
            if (provider is null)
                return ApiResponseDto<AmbulanceBookingResponseDto>.Fail(
                    $"ProviderId '{dto.ProviderId}' পাওয়া যায়নি।");

            // ── ③ AutoMapper দিয়ে Entity তৈরি ──
            var booking = mapper.Map<AmbulanceBooking>(dto);

            // ── ④ Business Logic fields ──
            booking.BookingCode = $"AMB-{Random.Shared.Next(100000, 999999)}";
            booking.Status = AmbulanceBookingStatus.Requested;
            booking.RequestedAt = DateTime.UtcNow;

            // ── ⑤ Vehicle Status → OnTrip করা ──
            vehicle.Status = AmbulanceStatus.OnTrip;
            await uow.AmbulanceVehicles.UpdateAsync(vehicle, cancellationToken);

            await uow.AmbulanceBookings.AddAsync(booking, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<AmbulanceBookingResponseDto>(booking);
            return ApiResponseDto<AmbulanceBookingResponseDto>.Ok(
                result, "Ambulance সফলভাবে Book করা হয়েছে।");
        }
    }

}
