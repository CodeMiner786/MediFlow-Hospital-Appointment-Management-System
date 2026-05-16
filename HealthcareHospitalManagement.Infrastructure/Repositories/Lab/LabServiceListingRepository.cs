using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Lab
{
    public class LabServiceListingRepository(ApplicationDbContext context)
        : GenericRepository<LabServiceListing>(context), ILabServiceListingRepository
    {
        private readonly DbSet<LabServiceListing> _dbSet = context.Set<LabServiceListing>();

        // 🔹 ল্যাব প্রোফাইলের আন্ডারে সব সার্ভিস লোড করা
        public async Task<IEnumerable<LabServiceListing>> GetByLabIdAsync(Guid labId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.LabProfileId == labId && !s.IsDeleted)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync(ct);
        }

        // 🔹 পাবলিক ফিডের জন্য সার্ভিসগুলো বের করা
        public async Task<IEnumerable<LabServiceListing>> GetActiveFeedServicesAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.LabProfile)
                .Where(s => s.IsActive && s.FeedStatus == FeedItemStatus.Active && !s.IsDeleted)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync(ct);
        }

        // 🔹 হোম কালেকশন সার্ভিসগুলো দেখতে
        public async Task<IEnumerable<LabServiceListing>> GetHomeCollectionServicesAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.HomeCollectionAvailable && s.IsActive && !s.IsDeleted)
                .Include(s => s.LabProfile)
                .ToListAsync(ct);
        }

        // 🔹 নির্দিষ্ট বাজেটের মধ্যে ল্যাব টেস্ট খোঁজা
        public async Task<IEnumerable<LabServiceListing>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.Price >= minPrice && s.Price <= maxPrice && s.IsActive && !s.IsDeleted)
                .OrderBy(s => s.Price)
                .ToListAsync(ct);
        }

        // 🔹 ড্যাশবোর্ড থেকে ফিড আইটেমের স্ট্যাটাস পরিবর্তন করা
        public async Task UpdateFeedStatusAsync(Guid serviceId, FeedItemStatus status, CancellationToken ct = default)
        {
            var service = await GetByIdAsync(serviceId, ct);
            if (service != null)
            {
                service.FeedStatus = status;
                await context.SaveChangesAsync(ct);
            }
        }
    }
}
