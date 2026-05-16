using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Wards;

namespace HealthcareHospitalManagement.Domain.Interfaces.Wards
{
    public interface IBedBookingRepository : IGenericRepository<BedBooking>
    {
        // ১. বুকিং কোড দিয়ে বুকিং ডিটেইলস খুঁজে বের করা
        Task<BedBooking?> GetByBookingCodeAsync(string bookingCode, CancellationToken ct = default);

        // ২. একজন ইউজারের (User Account) করা সব বুকিং লিস্ট দেখা
        Task<IEnumerable<BedBooking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken ct = default);

        // ৩. নির্দিষ্ট একজন পেশেন্টের জন্য করা বুকিংগুলো দেখা
        Task<IEnumerable<BedBooking>> GetBookingsByPatientIdAsync(Guid patientId, CancellationToken ct = default);

        // ৪. পেমেন্ট স্ট্যাটাস অনুযায়ী বুকিং ফিল্টার করা (e.g., Unpaid bookings)
        Task<IEnumerable<BedBooking>> GetBookingsByPaymentStatusAsync(bool isPaid, CancellationToken ct = default);

        // ৫. নির্দিষ্ট তারিখের চেক-ইন শিডিউল দেখা
        Task<IEnumerable<BedBooking>> GetTodaysCheckInsAsync(CancellationToken ct = default);

        // ৬. পেমেন্ট সাকসেসফুল হলে বুকিং আপডেট করা
        Task UpdatePaymentStatusAsync(Guid bookingId, bool status, CancellationToken ct = default);
    }
}
