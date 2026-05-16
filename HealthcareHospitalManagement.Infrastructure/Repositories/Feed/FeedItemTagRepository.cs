using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feed
{
    public class FeedItemTagRepository(ApplicationDbContext context)
        : GenericRepository<FeedItemTag>(context), IFeedItemTagRepository
    {
        private readonly DbSet<FeedItemTag> _dbSet = context.Set<FeedItemTag>();

        // 🔹 নির্দিষ্ট ফিড আইটেমের সাথে যুক্ত সব ট্যাগ স্ট্রিম করা
        public IAsyncEnumerable<FeedItemTag> GetTagsByFeedItemIdStream(Guid feedItemId)
        {
            return _dbSet
                .Where(t => t.FeedItemId == feedItemId && !t.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট নাম দিয়ে ট্যাগ সার্চ করা এবং সংশ্লিষ্ট ফিড আইটেমগুলো লোড করা
        public IAsyncEnumerable<FeedItemTag> GetFeedItemsByTagNameStream(string tagName)
        {
            return _dbSet
                .Where(t => t.Tag.ToLower() == tagName.ToLower() && !t.IsDeleted)
                .Include(t => t.FeedItem) // ট্যাগ দিয়ে ফিড আইটেম খুঁজে পেতে সাহায্য করবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 সবচেয়ে বেশি ব্যবহৃত ট্যাগগুলো খুঁজে বের করা (Trending Tags)
        public async Task<IEnumerable<string>> GetTrendingTagsAsync(int count)
        {
            return await _dbSet
                .Where(t => !t.IsDeleted)
                .GroupBy(t => t.Tag)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(count)
                .ToListAsync();
        }

        // 🔹 ফিড আইটেম থেকে নির্দিষ্ট ট্যাগ মুছে ফেলা
        public async Task RemoveTagAsync(Guid feedItemId, string tagName)
        {
            var tagEntry = await _dbSet.FirstOrDefaultAsync(t =>
                t.FeedItemId == feedItemId &&
                t.Tag.ToLower() == tagName.ToLower());

            if (tagEntry != null)
            {
                _dbSet.Remove(tagEntry);
                await context.SaveChangesAsync();
            }
        }
    }
}
