using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;

namespace HealthcareHospitalManagement.Domain.Interfaces.Ambulance
{
    public interface IAmbulanceProviderWalletRepository : IGenericRepository<AmbulanceProviderWallet>
    {
        // ProviderId দিয়ে নির্দিষ্ট ওয়ালেট খুঁজে বের করা
        Task<AmbulanceProviderWallet?> GetByProviderIdAsync(Guid providerId, CancellationToken ct = default);

        // ব্যালেন্স আপডেট করার জন্য একটি বিশেষ মেথড
        Task UpdateBalanceAsync(Guid providerId, decimal amount, CancellationToken ct = default);
    }
}
