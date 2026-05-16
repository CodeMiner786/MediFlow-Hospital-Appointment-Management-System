using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Lab
{
    public class LabProfileRepository(ApplicationDbContext context)
        : GenericRepository<LabProfile>(context), ILabProfileRepository
    {
        private readonly DbSet<LabProfile> _dbSet = context.Set<LabProfile>();

        // 🔹 ল্যাব অ্যাডমিন যখন লগইন করবে, তখন তার ল্যাব প্রোফাইল লোড করা
        public async Task<LabProfile?> GetByAdminUserIdAsync(Guid adminUserId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(l => l.ApplicationUser)
                .FirstOrDefaultAsync(l => l.ApplicationUserId == adminUserId && !l.IsDeleted, ct);
        }

        // 🔹 সুপার অ্যাডমিন ড্যাশবোর্ডের জন্য শুধুমাত্র ভেরিফাইড ল্যাবগুলো দেখানো
        public async Task<IEnumerable<LabProfile>> GetVerifiedLabsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(l => l.IsVerified && l.IsActive && !l.IsDeleted)
                .OrderByDescending(l => l.AverageRating)
                .ToListAsync(ct);
        }

        // 🔹 পেশেন্ট যখন নির্দিষ্ট এরিয়ায় ল্যাব খুঁজবে
        public async Task<IEnumerable<LabProfile>> GetActiveLabsByCityAsync(string city, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(l => string.Equals(l.City, city, StringComparison.OrdinalIgnoreCase)
                            && l.IsActive && !l.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 ল্যাবের পারফরম্যান্স ট্র্যাক করার জন্য রেটিং আপডেট
        public async Task UpdateLabRatingAsync(Guid labId, decimal newRating, CancellationToken ct = default)
        {
            var lab = await GetByIdAsync(labId, ct);
            if (lab != null)
            {
                lab.TotalRatings += 1;
                lab.AverageRating = ((lab.AverageRating * (lab.TotalRatings - 1)) + newRating) / lab.TotalRatings;

                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 নতুন ল্যাব খোলার সময় রেজিস্ট্রেশন নাম্বার চেক করা
        public async Task<bool> IsRegistrationNumberUniqueAsync(string regNumber, CancellationToken ct = default)
        {
            return !await _dbSet.AnyAsync(l => l.RegistrationNumber == regNumber && !l.IsDeleted, ct);
        }
    }
}
