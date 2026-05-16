using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.OTP;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class OtpRepository(ApplicationDbContext context)
        : GenericRepository<OtpCode>(context), IOtpRepository
    {
        private readonly DbSet<OtpCode> _dbSet = context.Set<OtpCode>();

        // 🔹 ইউজারের জন্য সবথেকে রিসেন্ট এবং এখনো ভ্যালিড এমন OTP খুঁজে বের করা
        public async Task<OtpCode?> GetLatestValidOtpAsync(Guid userId, OtpPurpose purpose)
        {
            return await _dbSet
                .Where(o => o.UserId == userId &&
                            o.Purpose == purpose &&
                            !o.IsUsed &&
                            o.ExpiresAt > DateTime.UtcNow &&
                            o.AttemptCount < o.MaxAttempts)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }

        // 🔹 নতুন OTP পাঠানোর আগে আগের সব আন-ইউজড OTP ইনভ্যালিড (IsUsed = true) করে দেওয়া
        public async Task InvalidateOldOtpsAsync(Guid userId, OtpPurpose purpose)
        {
            var oldOtps = await _dbSet
                .Where(o => o.UserId == userId && o.Purpose == purpose && !o.IsUsed)
                .ToListAsync();

            foreach (var otp in oldOtps)
            {
                otp.IsUsed = true; // এটাকে 'Expired' বা 'Invalid' হিসেবে মার্ক করা হচ্ছে
            }

            await context.SaveChangesAsync();
        }

        // 🔹 ইউজার ভুল কোড দিলে অ্যাটেম্পট বাড়ানো (Brute-force প্রোটেকশন)
        public async Task IncrementAttemptAsync(Guid otpId)
        {
            var otp = await _dbSet.FindAsync(otpId);
            if (otp != null)
            {
                otp.AttemptCount++;
                await context.SaveChangesAsync();
            }
        }

        // 🔹 কোডটি সফলভাবে ভেরিফাই হলে সেটিকে ব্যবহৃত (Used) হিসেবে মার্ক করা
        public async Task MarkAsUsedAsync(Guid otpId)
        {
            var otp = await _dbSet.FindAsync(otpId);
            if (otp != null)
            {
                otp.IsUsed = true;
                otp.UsedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
        }

        // 🔹 ডাটাবেজ হালকা রাখার জন্য এক্সপায়ারড OTP গুলো ডিলিট করা
        public async Task DeleteExpiredOtpsAsync()
        {
            var expiredOtps = _dbSet.Where(o => o.ExpiresAt < DateTime.UtcNow || o.IsUsed);
            _dbSet.RemoveRange(expiredOtps);
            await context.SaveChangesAsync();
        }
    }
}
