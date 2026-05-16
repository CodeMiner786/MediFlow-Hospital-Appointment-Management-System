using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class UserLoginHistoryRepository(ApplicationDbContext context)
        : GenericRepository<UserLoginHistory>(context), IUserLoginHistoryRepository
    {
        private readonly DbSet<UserLoginHistory> _dbSet = context.Set<UserLoginHistory>();

        // 🔹 ইউজারের সব লগইন রেকর্ড স্ট্রিম করা (যাতে মেমোরি কম লাগে)
        public IAsyncEnumerable<UserLoginHistory> GetLogsByUserIdStream(Guid userId)
        {
            return _dbSet
                .Where(l => l.UserId == userId && !l.IsDeleted)
                .OrderByDescending(l => l.LoginAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 ইউজারের সাম্প্রতিক সফল লগইনগুলো দেখা (ইউজারকে "Last Login" দেখানোর জন্য)
        public async Task<IEnumerable<UserLoginHistory>> GetRecentSuccessLoginsAsync(Guid userId, int count = 5)
        {
            return await _dbSet
                .Where(l => l.UserId == userId && l.IsSuccess && !l.IsDeleted)
                .OrderByDescending(l => l.LoginAt)
                .Take(count)
                .ToListAsync();
        }

        // 🔹 সিকিউরিটি চেক: নির্দিষ্ট সময় ধরে একটি আইপি থেকে কতবার ব্যর্থ লগইন হয়েছে
        public async Task<int> GetFailedCountByIpAsync(string ipAddress, DateTime since)
        {
            return await _dbSet.CountAsync(l =>
                l.IpAddress == ipAddress &&
                !l.IsSuccess &&
                l.LoginAt >= since);
        }

        // 🔹 নির্দিষ্ট দেশের লগইন ট্র্যাকিং (Anomalous detection এর জন্য)
        public async Task<IEnumerable<UserLoginHistory>> GetLogsByCountryAsync(string countryCode)
        {
            return await _dbSet
                .Where(l => l.Country == countryCode && !l.IsDeleted)
                .ToListAsync();
        }

        // 🔹 ডাটাবেজ অপ্টিমাইজেশন: অনেক পুরানো হিস্টোরি ডিলিট করা
        public async Task DeleteOldHistoryAsync(DateTime beforeDate)
        {
            var oldLogs = _dbSet.Where(l => l.LoginAt < beforeDate);
            _dbSet.RemoveRange(oldLogs);
            await context.SaveChangesAsync();
        }
    }
}
