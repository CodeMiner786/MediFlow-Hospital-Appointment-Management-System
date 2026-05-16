using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Interfaces.Payment;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Payment
{
    public class PlatformWalletRepository(ApplicationDbContext context)
        : GenericRepository<PlatformWallet>(context), IPlatformWalletRepository
    {
        private readonly DbSet<PlatformWallet> _dbSet = context.Set<PlatformWallet>();

        // 🔹 সাধারণত সিস্টেমে একটাই প্ল্যাটফর্ম ওয়ালেট থাকে, তাই প্রথমটি রিটার্ন করা হয়
        public async Task<PlatformWallet?> GetMainWalletAsync()
        {
            return await _dbSet
                .Include(w => w.Transactions.OrderByDescending(t => t.CreatedAt).Take(10)) // শেষ ১০টি ট্রানজেকশনসহ
                .FirstOrDefaultAsync(w => !w.IsDeleted);
        }

        // 🔹 ব্যালেন্স ক্যালকুলেশন: টাকা আসলে Credit (Add), রিফান্ড হলে Debit (Subtract)
        public async Task UpdateBalanceAsync(decimal amount, bool isCredit)
        {
            var wallet = await GetMainWalletAsync();
            if (wallet == null) return;

            if (isCredit)
            {
                wallet.TotalBalance += amount;
                wallet.TotalReceived += amount;
            }
            else
            {
                wallet.TotalBalance -= amount;
                wallet.TotalRefunded += amount;
            }

            wallet.LastTransactionAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }

        // 🔹 টোটাল রিফান্ড হিসেব দেখা
        public async Task<decimal> GetTotalRefundedAmountAsync()
        {
            var wallet = await GetMainWalletAsync();
            return wallet?.TotalRefunded ?? 0;
        }

        // 🔹 ট্রানজেকশন টাইমস্ট্যাম্প আপডেট
        public async Task UpdateLastTransactionDateAsync(DateTime transactionDate)
        {
            var wallet = await GetMainWalletAsync();
            if (wallet != null)
            {
                wallet.LastTransactionAt = transactionDate;
                await context.SaveChangesAsync();
            }
        }
    }
}
