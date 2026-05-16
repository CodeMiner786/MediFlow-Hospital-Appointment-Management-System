using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IHospitalSettingsRepository : IGenericRepository<HospitalSettings>
    {
        // সিস্টেমের বর্তমান একটিভ সেটিংসটি খুঁজে বের করা
        Task<HospitalSettings?> GetCurrentSettingsAsync(CancellationToken ct = default);

        // ডিফল্ট শেয়ার পারসেন্টেজ আপডেট করা
        Task UpdateDefaultSharesAsync(decimal platformShare, decimal doctorShare, CancellationToken ct = default);
    }
}
