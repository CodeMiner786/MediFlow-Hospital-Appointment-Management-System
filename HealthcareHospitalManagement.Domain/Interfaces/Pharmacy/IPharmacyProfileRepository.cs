using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface IPharmacyProfileRepository : IGenericRepository<PharmacyProfile>
    {
        // ১. ইউজার আইডি (ApplicationUserId) দিয়ে প্রোফাইল খুঁজে বের করা
        Task<PharmacyProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);

        // ২. লাইসেন্স নম্বর দিয়ে ফার্মেসি ভেরিফাই করা
        Task<PharmacyProfile?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken ct = default);

        // ৩. নির্দিষ্ট সিটির সব একটিভ ফার্মেসি খুঁজে বের করা
        Task<IEnumerable<PharmacyProfile>> GetActivePharmaciesByCityAsync(string city, CancellationToken ct = default);

        // ৪. ডেলিভারি সার্ভিস আছে এমন ভেরিফাইড ফার্মেসিগুলো দেখা
        Task<IEnumerable<PharmacyProfile>> GetVerifiedPharmaciesWithDeliveryAsync(CancellationToken ct = default);

        // ৫. ফার্মেসির রেটিং আপডেট করা
        Task UpdatePharmacyRatingAsync(Guid pharmacyId, decimal newRating, CancellationToken ct = default);

        // ৬. ফার্মেসির ভেরিফিকেশন স্ট্যাটাস পরিবর্তন করা (Admin Only)
        Task UpdateVerificationStatusAsync(Guid pharmacyId, bool isVerified, CancellationToken ct = default);
    }
}
