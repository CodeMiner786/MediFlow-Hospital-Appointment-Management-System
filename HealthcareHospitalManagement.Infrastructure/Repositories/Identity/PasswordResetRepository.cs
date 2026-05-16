using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class PasswordResetRepository(ApplicationDbContext context)
        : GenericRepository<PasswordResetRequest>(context), IPasswordResetRepository
    {
        private readonly DbSet<PasswordResetRequest> _dbSet = context.Set<PasswordResetRequest>();

        // 🔹 ডাটাবেজ থেকে হ্যাশ করা টোকেন দিয়ে রিকোয়েস্ট খুঁজে বের করা
        public async Task<PasswordResetRequest?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.TokenHash == tokenHash && !r.IsDeleted, ct);
        }

        // 🔹 একই ইউজার বারবার রিকোয়েস্ট করলে আগেরগুলো ইনভ্যালিড করে দেওয়া
        public async Task InvalidateOldRequestsAsync(Guid userId, CancellationToken ct = default)
        {
            var oldRequests = await _dbSet
                .Where(r => r.UserId == userId && !r.IsUsed)
                .ToListAsync(ct);

            foreach (var request in oldRequests)
            {
                request.IsUsed = true;
            }

            await context.SaveChangesAsync(ct);
        }

        // 🔹 পাসওয়ার্ড সফলভাবে রিসেট হলে রিকোয়েস্ট ক্লোজ করা
        public async Task MarkAsUsedAsync(Guid requestId, CancellationToken ct = default)
        {
            var request = await GetByIdAsync(requestId, ct);
            if (request != null)
            {
                request.IsUsed = true;
                request.UsedAt = DateTime.UtcNow;
                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 স্প্যামিং রোধে চেক করা যে ইউজারের কোনো একটিভ টোকেন আছে কি না
        public async Task<bool> HasActiveRequestAsync(Guid userId, CancellationToken ct = default)
        {
            return await _dbSet.AnyAsync(r =>
                r.UserId == userId &&
                !r.IsUsed &&
                r.ExpiresAt > DateTime.UtcNow, ct);
        }
    }
}
