using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Lab;

namespace HealthcareHospitalManagement.Domain.Interfaces.Lab
{
    public interface ILabProfileRepository : IGenericRepository<LabProfile>
    {
        // ১. অ্যাপ্লিকেশন ইউজার আইডি (Admin) দিয়ে ল্যাব প্রোফাইল খুঁজে বের করা
        Task<LabProfile?> GetByAdminUserIdAsync(Guid adminUserId, CancellationToken ct = default);

        // ২. ভেরিফিকেশন স্ট্যাটাস অনুযায়ী ল্যাব ফিল্টার করা
        Task<IEnumerable<LabProfile>> GetVerifiedLabsAsync(CancellationToken ct = default);

        // ৩. নির্দিষ্ট সিটি (City) অনুযায়ী একটিভ ল্যাবগুলোর লিস্ট
        Task<IEnumerable<LabProfile>> GetActiveLabsByCityAsync(string city, CancellationToken ct = default);

        // ৪. ল্যাবের রেটিং আপডেট করা (পেশেন্ট ফিডব্যাকের পর)
        Task UpdateLabRatingAsync(Guid labId, decimal newRating, CancellationToken ct = default);

        // ৫. রেজিস্ট্রেশন নাম্বার দিয়ে ল্যাব ভেরিফাই করা
        Task<bool> IsRegistrationNumberUniqueAsync(string regNumber, CancellationToken ct = default);
    }
}
