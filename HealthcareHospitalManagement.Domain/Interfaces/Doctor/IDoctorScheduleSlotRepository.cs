using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorScheduleSlotRepository : IGenericRepository<DoctorScheduleSlot>
    {
        // নির্দিষ্ট একজন ডাক্তারের একটি নির্দিষ্ট তারিখের সব স্লট স্ট্রীম করা
        IAsyncEnumerable<DoctorScheduleSlot> GetSlotsByDoctorAndDateStream(Guid doctorId, DateTime date);

        // বুকিংয়ের জন্য শুধুমাত্র ফাঁকা (Available) স্লটগুলো খুঁজে বের করা
        IAsyncEnumerable<DoctorScheduleSlot> GetAvailableSlotsStream(Guid doctorId, DateTime date);

        // একটি নির্দিষ্ট শিডিউলের অধীনে থাকা সব স্লট ডিলিট বা আপডেট করার জন্য মেথড
        Task<IEnumerable<DoctorScheduleSlot>> GetSlotsByScheduleIdAsync(Guid scheduleId, CancellationToken ct = default);

        // স্লট বুক করার সময় সেটিকে 'Booked' হিসেবে মার্ক করা
        Task<bool> MarkSlotAsBookedAsync(Guid slotId, Guid appointmentId, CancellationToken ct = default);

        // স্লট ব্লক করার মেথড (যেমন: ডাক্তার ওই সময়ে ব্যক্তিগত কাজে ব্যস্ত থাকলে)
        Task MarkSlotAsBlockedAsync(Guid slotId, string reason, CancellationToken ct = default);

        // নির্দিষ্ট সময়ের ব্যবধানে কোনো স্লট খালি আছে কি না চেক করা
        Task<bool> IsSlotAvailableAsync(Guid slotId, CancellationToken ct = default);
    }
}
