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
    public class FeedItemLikeRepository(ApplicationDbContext context)
        : GenericRepository<FeedItemLike>(context), IFeedItemLikeRepository
    {
        private readonly DbSet<FeedItemLike> _dbSet = context.Set<FeedItemLike>();

        public async Task<int> GetLikeCountByFeedItemIdAsync(Guid feedItemId)
        {
            return await _dbSet.CountAsync(l => l.FeedItemId == feedItemId && !l.IsDeleted);
        }

        public async Task<bool> IsUserLikedAsync(Guid feedItemId, Guid userId)
        {
            return await _dbSet.AnyAsync(l => l.FeedItemId == feedItemId && l.UserId == userId && !l.IsDeleted);
        }

        public IAsyncEnumerable<FeedItemLike> GetLikesByFeedItemIdStream(Guid feedItemId)
        {
            return _dbSet
                .Where(l => l.FeedItemId == feedItemId && !l.IsDeleted)
                .Include(l => l.User) // ইউজারের তথ্য দেখানোর জন্য
                .OrderByDescending(l => l.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<FeedItemLike> GetUserLikedItemsStream(Guid userId)
        {
            return _dbSet
                .Where(l => l.UserId == userId && !l.IsDeleted)
                .Include(l => l.FeedItem)
                .OrderByDescending(l => l.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task RemoveLikeAsync(Guid feedItemId, Guid userId)
        {
            var like = await _dbSet.FirstOrDefaultAsync(l => l.FeedItemId == feedItemId && l.UserId == userId);
            if (like is not null)
            {
                _dbSet.Remove(like);
                await context.SaveChangesAsync();
            }
        }

        public async Task<FeedItemLike?> GetByFeedItemAndUserAsync(Guid feedItemId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(
                l => l.FeedItemId == feedItemId && l.UserId == userId && !l.IsDeleted,
                cancellationToken);
        }

        public async Task RemoveAsync(FeedItemLike entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
