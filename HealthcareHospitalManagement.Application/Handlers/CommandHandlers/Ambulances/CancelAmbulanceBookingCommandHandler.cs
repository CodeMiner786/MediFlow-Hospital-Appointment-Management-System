using HealthcareHospitalManagement.Application.Commands.Ambulances;
using HealthcareHospitalManagement.Application.Common;
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
    public sealed class CancelAmbulanceBookingCommandHandler(IAmbulanceUnitOfWork uow)
    : IRequestHandler<CancelAmbulanceBookingCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            CancelAmbulanceBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await uow.AmbulanceBookings.GetByIdAsync(request.BookingId, cancellationToken);
            if (booking is null)
                return ApiResponseDto<bool>.Fail(
                    $"BookingId '{request.BookingId}' পাওয়া যায়নি।");

            // ── Completed বা আগেই Cancelled হলে Cancel করা যাবে না ──
            if (booking.Status == AmbulanceBookingStatus.Completed)
                return ApiResponseDto<bool>.Fail("Completed Booking Cancel করা যাবে না।");

            if (booking.Status == AmbulanceBookingStatus.Cancelled)
                return ApiResponseDto<bool>.Fail("এই Booking ইতিমধ্যেই Cancelled আছে।");

            booking.Status = AmbulanceBookingStatus.Cancelled;
            booking.CancellationReason = request.CancellationReason;
            booking.UpdatedAt = DateTime.UtcNow;

            // ── Vehicle → Available করা ──
            var vehicle = await uow.AmbulanceVehicles.GetByIdAsync(booking.VehicleId, cancellationToken);
            if (vehicle is not null)
            {
                vehicle.Status = AmbulanceStatus.Available;
                await uow.AmbulanceVehicles.UpdateAsync(vehicle, cancellationToken);
            }

            await uow.AmbulanceBookings.UpdateAsync(booking, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Booking সফলভাবে Cancel করা হয়েছে।");
        }
    }

}
