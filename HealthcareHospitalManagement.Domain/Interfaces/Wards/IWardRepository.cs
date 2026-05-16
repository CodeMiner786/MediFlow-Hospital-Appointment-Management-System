using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Interfaces.Wards
{
    public interface IWardRepository : IGenericRepository<Ward>
    {
        // ১. ওয়ার্ড কোড দিয়ে ওয়ার্ডের তথ্য বের করা
        Task<Ward?> GetByCodeAsync(string wardCode, CancellationToken ct = default);

        // ২. নির্দিষ্ট ফ্লোর অনুযায়ী সব ওয়ার্ড দেখা
        Task<IEnumerable<Ward>> GetWardsByFloorAsync(int floorNumber, CancellationToken ct = default);

        // ৩. নির্দিষ্ট টাইপের সব ওয়ার্ড খুঁজে বের করা (e.g., ICU, Pediatric)
        Task<IEnumerable<Ward>> GetWardsByTypeAsync(WardType wardType, CancellationToken ct = default);

        // ৪. যেসব ওয়ার্ডে এখনো খালি বেড (AvailableBeds > 0) আছে
        Task<IEnumerable<Ward>> GetWardsWithAvailableBedsAsync(CancellationToken ct = default);

        // ৫. ওয়ার্ডের বেড সংখ্যা রিয়েল-টাইমে আপডেট করা
        Task UpdateBedCountsAsync(Guid wardId, CancellationToken ct = default);

        // ৬. বিল্ডিং অনুযায়ী ওয়ার্ডগুলো ফিল্টার করা
        Task<IEnumerable<Ward>> GetWardsByBuildingAsync(string buildingName, CancellationToken ct = default);
    }
}
