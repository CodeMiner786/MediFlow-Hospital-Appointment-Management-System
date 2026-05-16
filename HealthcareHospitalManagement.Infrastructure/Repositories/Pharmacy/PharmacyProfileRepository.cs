using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class PharmacyProfileRepository(ApplicationDbContext context)
        : GenericRepository<PharmacyProfile>(context), IPharmacyProfileRepository
    {
        private readonly DbSet<PharmacyProfile> _dbSet = context.Set<PharmacyProfile>();

        // 🔹 ফার্মেসি অ্যাডমিন লগইন করলে তার প্রোফাইল লোড করার জন্য
        public async Task<PharmacyProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.ApplicationUserId == userId && !p.IsDeleted, ct);
        }

        // 🔹 ডুপ্লিকেট লাইসেন্স চেক বা ভেরিফিকেশনের জন্য
        public async Task<PharmacyProfile?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.LicenseNumber == licenseNumber && !p.IsDeleted, ct);
        }

        // 🔹 পেশেন্ট যখন এরিয়া অনুযায়ী ফার্মেসি খুঁজবে
        public async Task<IEnumerable<PharmacyProfile>> GetActivePharmaciesByCityAsync(string city, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => string.Equals(p.City, city, StringComparison.OrdinalIgnoreCase)
                            && p.IsActive && !p.IsDeleted)
                .OrderByDescending(p => p.AverageRating)
                .ToListAsync(ct);
        }

        // 🔹 হোম ডেলিভারি ফিল্টার (পেশেন্ট অ্যাপের জন্য)
        public async Task<IEnumerable<PharmacyProfile>> GetVerifiedPharmaciesWithDeliveryAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.IsVerified && p.HasDeliveryService && p.IsActive && !p.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 পেশেন্ট ফিডব্যাকের পর রেটিং ক্যালকুলেশন ও আপডেট
        public async Task UpdatePharmacyRatingAsync(Guid pharmacyId, decimal newRating, CancellationToken ct = default)
        {
            var pharmacy = await GetByIdAsync(pharmacyId, ct);
            if (pharmacy != null)
            {
                // নতুন গড় রেটিং হিসেব করার লজিক (সিম্পল আপডেট)
                pharmacy.TotalRatings += 1;
                pharmacy.AverageRating = ((pharmacy.AverageRating * (pharmacy.TotalRatings - 1)) + newRating) / pharmacy.TotalRatings;

                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 সুপার-অ্যাডমিন দ্বারা ফার্মেসি এপ্রুভাল
        public async Task UpdateVerificationStatusAsync(Guid pharmacyId, bool isVerified, CancellationToken ct = default)
        {
            var pharmacy = await GetByIdAsync(pharmacyId, ct);
            if (pharmacy != null)
            {
                pharmacy.IsVerified = isVerified;
                await context.SaveChangesAsync(ct);
            }
        }
    }
}
