using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.Ambulance;

namespace HealthcareHospitalManagement.Domain.Interfaces.Ambulance;

public interface IAmbulanceWalletTransactionRepository : IGenericRepository<AmbulanceWalletTransaction>
{
    // 🔹 ট্রানজ্যাকশন কোড দিয়ে খোঁজা
    Task<AmbulanceWalletTransaction?> GetByTransactionCodeAsync(string transactionCode, CancellationToken ct = default);

    // 🔹 নির্দিষ্ট ওয়ালেটের সব ট্রানজ্যাকশন
    Task<IEnumerable<AmbulanceWalletTransaction>> GetByWalletIdAsync(Guid walletId, CancellationToken ct = default);

    // 🔹 নির্দিষ্ট বুকিংয়ের ট্রানজ্যাকশন
    Task<IEnumerable<AmbulanceWalletTransaction>> GetByBookingIdAsync(Guid bookingId, CancellationToken ct = default);

    // 🔹 ট্রানজ্যাকশন টাইপ অনুযায়ী (stream)
    IAsyncEnumerable<AmbulanceWalletTransaction> GetByTransactionTypeStream(WalletTransactionType type, CancellationToken ct = default);

    // 🔹 তারিখের রেঞ্জ অনুযায়ী ট্রানজ্যাকশন
    Task<IEnumerable<AmbulanceWalletTransaction>> GetByDateRangeAsync(Guid walletId, DateTime from, DateTime to, CancellationToken ct = default);
}
