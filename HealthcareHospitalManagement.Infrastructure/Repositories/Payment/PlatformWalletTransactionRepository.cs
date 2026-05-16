using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Interfaces.Payment;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Payment
{
    public class PlatformWalletTransactionRepository(ApplicationDbContext context)
        : GenericRepository<PlatformWalletTransaction>(context), IPlatformWalletTransactionRepository
    {
        private readonly DbSet<PlatformWalletTransaction> _dbSet = context.Set<PlatformWalletTransaction>();

        // 🔹 ওয়ালেট আইডি অনুযায়ী সব লেজার এন্ট্রি লোড করা
        public async Task<IEnumerable<PlatformWalletTransaction>> GetTransactionsByWalletIdAsync(Guid walletId)
        {
            return await _dbSet
                .Where(t => t.WalletId == walletId && !t.IsDeleted)
                .OrderByDescending(t => t.TransactionAt)
                .ToListAsync();
        }

        // 🔹 নির্দিষ্ট ক্যাটাগরির ট্রানজেকশন ফিল্টার (যেমন: ল্যাব পেমেন্ট কত হলো তা দেখা)
        public async Task<IEnumerable<PlatformWalletTransaction>> GetTransactionsByTypeAsync(string transactionType)
        {
            return await _dbSet
                .Where(t => t.TransactionType == transactionType && !t.IsDeleted)
                .ToListAsync();
        }

        // 🔹 পেমেন্ট মডিউলের সাথে ভেরিফাই করার জন্য পেমেন্ট আইডি দিয়ে খোঁজা
        public async Task<PlatformWalletTransaction?> GetByPaymentIdAsync(Guid paymentId)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.PaymentId == paymentId && !t.IsDeleted);
        }

        // 🔹 মান্থলি বা উইকলি স্টেটমেন্ট জেনারেট করার জন্য
        public async Task<IEnumerable<PlatformWalletTransaction>> GetStatementByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(t => t.TransactionAt >= start && t.TransactionAt <= end && !t.IsDeleted)
                .OrderByDescending(t => t.TransactionAt)
                .ToListAsync();
        }

        // 🔹 ব্যালেন্স ক্যালকুলেশনের সুবিধার জন্য লেটেস্ট রেকর্ডটি বের করা
        public async Task<PlatformWalletTransaction?> GetLatestTransactionAsync(Guid walletId)
        {
            return await _dbSet
                .Where(t => t.WalletId == walletId && !t.IsDeleted)
                .OrderByDescending(t => t.TransactionAt)
                .FirstOrDefaultAsync();
        }
    }
}
