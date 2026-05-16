using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class PharmacyServiceListingRepository(ApplicationDbContext context)
        : GenericRepository<PharmacyServiceListing>(context), IPharmacyServiceListingRepository
    {
        private readonly DbSet<PharmacyServiceListing> _dbSet = context.Set<PharmacyServiceListing>();

        // 🔹 ফার্মেসি অ্যাডমিন যখন নিজের ড্যাশবোর্ডে তার প্রমোশনগুলো দেখবে
        public async Task<IEnumerable<PharmacyServiceListing>> GetByPharmacyIdAsync(Guid pharmacyId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(l => l.PharmacyProfileId == pharmacyId && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(ct);
        }

        // 🔹 পেশেন্ট অ্যাপের মেইন ফিডে দেখানোর জন্য
        public async Task<IEnumerable<PharmacyServiceListing>> GetActiveFeedItemsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Include(l => l.PharmacyProfile)
                .Where(l => l.IsActive &&
                            l.FeedStatus == FeedItemStatus.Active &&
                            !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(ct);
        }

        // 🔹 হোম ডেলিভারি স্পেশাল অফার ফিল্টার
        public async Task<IEnumerable<PharmacyServiceListing>> GetDeliveryServiceListingsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(l => l.IsDeliveryAvailable && l.IsActive && !l.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 অ্যাডমিন প্যানেল থেকে ভেরিফিকেশনের জন্য স্ট্যাটাস অনুযায়ী খোঁজা
        public async Task<IEnumerable<PharmacyServiceListing>> GetByFeedStatusAsync(FeedItemStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(l => l.FeedStatus == status && !l.IsDeleted)
                .Include(l => l.PharmacyProfile)
                .ToListAsync(ct);
        }

        // 🔹 লিস্টিং সার্চ (e.g., "Vaccine" লিখে সার্চ করলে সব ভ্যাকসিন অফার আসবে)
        public async Task<IEnumerable<PharmacyServiceListing>> SearchListingsAsync(string titlePart, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(l => l.ServiceTitle.Contains(titlePart) && l.IsActive && !l.IsDeleted)
                .ToListAsync(ct);
        }
    }
}
