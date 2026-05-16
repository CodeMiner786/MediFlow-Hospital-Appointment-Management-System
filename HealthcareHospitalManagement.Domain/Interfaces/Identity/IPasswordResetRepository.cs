using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IPasswordResetRepository : IGenericRepository<PasswordResetRequest>
    {
        // ১. টোকেন হ্যাশ দিয়ে রিকোয়েস্ট খুঁজে বের করা
        Task<PasswordResetRequest?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default);

        // ২. নতুন রিকোয়েস্ট পাঠানোর আগে আগের সব রিকোয়েস্ট ইনভ্যালিড করা
        Task InvalidateOldRequestsAsync(Guid userId, CancellationToken ct = default);

        // ৩. টোকেনটি সফলভাবে ব্যবহার হলে আপডেট করা
        Task MarkAsUsedAsync(Guid requestId, CancellationToken ct = default);

        // ৪. নির্দিষ্ট ইউজারের জন্য পেন্ডিং (Valid) রিকোয়েস্ট আছে কি না দেখা
        Task<bool> HasActiveRequestAsync(Guid userId, CancellationToken ct = default);
    }
}
