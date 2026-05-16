using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.Ambulance;
using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance
{
    public class AmbulanceWalletTransactionRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceWalletTransaction>(context), IAmbulanceWalletTransactionRepository
    {
        private readonly DbSet<AmbulanceWalletTransaction> _dbSet = context.Set<AmbulanceWalletTransaction>();

        // 🔹 ট্রানজ্যাকশন কোড দিয়ে খোঁজা
        public async Task<AmbulanceWalletTransaction?> GetByTransactionCodeAsync(
            string transactionCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(t => t.Wallet)
                .FirstOrDefaultAsync(t => t.TransactionCode == transactionCode && !t.IsDeleted, ct);
        }

        // 🔹 নির্দিষ্ট ওয়ালেটের সব ট্রানজ্যাকশন
        public async Task<IEnumerable<AmbulanceWalletTransaction>> GetByWalletIdAsync(
            Guid walletId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.WalletId == walletId && !t.IsDeleted)
                .OrderByDescending(t => t.TransactionAt)
                .ToListAsync(ct);
        }

        // 🔹 নির্দিষ্ট বুকিংয়ের ট্রানজ্যাকশন
        public async Task<IEnumerable<AmbulanceWalletTransaction>> GetByBookingIdAsync(
            Guid bookingId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.BookingId == bookingId && !t.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 ট্রানজ্যাকশন টাইপ অনুযায়ী (stream)
        public IAsyncEnumerable<AmbulanceWalletTransaction> GetByTransactionTypeStream(
            WalletTransactionType type, CancellationToken ct = default)
        {
            return _dbSet
                .Where(t => t.Type == type && !t.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable(); // cancellation consumer side এ handle হবে
        }

        // 🔹 তারিখের রেঞ্জ অনুযায়ী ট্রানজ্যাকশন
        public async Task<IEnumerable<AmbulanceWalletTransaction>> GetByDateRangeAsync(
            Guid walletId, DateTime from, DateTime to, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.WalletId == walletId &&
                            t.TransactionAt >= from &&
                            t.TransactionAt <= to &&
                            !t.IsDeleted)
                .OrderByDescending(t => t.TransactionAt)
                .ToListAsync(ct);
        }
    }
}
