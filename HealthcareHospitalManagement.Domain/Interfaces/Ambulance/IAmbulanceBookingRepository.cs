using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;

namespace HealthcareHospitalManagement.Domain.Interfaces.Ambulance
{
    public interface IAmbulanceBookingRepository : IGenericRepository<AmbulanceBooking>
    {
        // বুকিং কোড দিয়ে সার্চ
        Task<AmbulanceBooking?> GetByBookingCodeAsync(string bookingCode, CancellationToken ct);

        // একটিভ বুকিংগুলো স্ট্রিম আকারে পাওয়া
        IAsyncEnumerable<AmbulanceBooking> GetActiveBookingsStream(CancellationToken ct);
    }
}
