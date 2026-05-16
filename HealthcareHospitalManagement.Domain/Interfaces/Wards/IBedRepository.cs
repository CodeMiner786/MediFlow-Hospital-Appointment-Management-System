using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Interfaces.Wards
{
    public interface IBedRepository : IGenericRepository<Bed>
    {
        // ১. একটি নির্দিষ্ট ওয়ার্ডের (e.g., ICU) সব বেড দেখা
        Task<IEnumerable<Bed>> GetBedsByWardIdAsync(Guid wardId, CancellationToken ct = default);

        // ২. বর্তমানে খালি (Available) আছে এমন বেডগুলো খুঁজে বের করা
        Task<IEnumerable<Bed>> GetAvailableBedsByWardAsync(Guid wardId, CancellationToken ct = default);

        // ৩. নির্দিষ্ট ফ্যাসিলিটি অনুযায়ী বেড ফিল্টার (e.g., অক্সিজেন বা মনিটর আছে এমন)
        Task<IEnumerable<Bed>> GetBedsByFacilityAsync(bool hasOxygen, bool hasMonitor, CancellationToken ct = default);

        // ৪. বেডের স্ট্যাটাস আপডেট করা (e.g., Occupied, Under Maintenance)
        Task UpdateBedStatusAsync(Guid bedId, BedStatus status, CancellationToken ct = default);

        // ৫. একটি ওয়ার্ডে মোট কতগুলো খালি বেড আছে তার সংখ্যা জানা
        Task<int> GetAvailableBedCountByWardAsync(Guid wardId, CancellationToken ct = default);

        // ৬. আইসোলেশন বেডগুলো আলাদাভাবে দেখা
        Task<IEnumerable<Bed>> GetIsolationBedsAsync(CancellationToken ct = default);
    }
}
