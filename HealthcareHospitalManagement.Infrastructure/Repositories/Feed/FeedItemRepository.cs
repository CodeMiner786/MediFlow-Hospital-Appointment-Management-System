using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Enums.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feed
{
    // Primary Constructor ব্যবহার করে context এবং GenericRepository ইনহেরিট করা হয়েছে
    public class FeedItemRepository(ApplicationDbContext context)
        : GenericRepository<FeedItem>(context), IFeedItemRepository
    {
        private readonly DbSet<FeedItem> _dbSet = context.Set<FeedItem>();

        // 🔹 নির্দিষ্ট ইউজারের করা সব ফিড পোস্ট স্ট্রিম আকারে আনা
        public IAsyncEnumerable<FeedItem> GetFeedByUserIdStream(Guid userId)
        {
            return _dbSet
                .Where(f => f.OwnerUserId == userId && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 টাইপ অনুযায়ী (যেমন: Post, Service) একটিভ ফিডগুলো ফিল্টার করা
        public IAsyncEnumerable<FeedItem> GetActiveFeedsByTypeStream(FeedItemType type)
        {
            return _dbSet
                .Where(f => f.ItemType == type
                         && f.Status == FeedItemStatus.Active
                         && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট শহরের ফিড আইটেমগুলো খুঁজে বের করা
        public IAsyncEnumerable<FeedItem> GetFeedsByCityStream(string city)
        {
            return _dbSet
                .Where(f => f.City == city && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 সবচেয়ে বেশি রেটিং পাওয়া ফিডগুলো স্ট্রিম করা
        public IAsyncEnumerable<FeedItem> GetTopRatedFeedsStream(int count)
        {
            return _dbSet
                .Where(f => !f.IsDeleted && f.Status == FeedItemStatus.Active)
                .OrderByDescending(f => f.AverageRating)
                .Take(count)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 ফিড আইটেমের সাথে রিলেটেড ডেটা (Tags, Likes, Posts) সহ লোড করা
        public async Task<FeedItem?> GetFeedWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(f => f.Tags)
                .Include(f => f.Likes)
                .Include(f => f.Saves)
                .Include(f => f.LinkedPosts)
                .Include(f => f.OwnerUser)
                .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);
        }

        // 🔹 Update method (synchronous)
        public void Update(FeedItem entity)
        {
            _dbSet.Update(entity);
        }
    }
}
