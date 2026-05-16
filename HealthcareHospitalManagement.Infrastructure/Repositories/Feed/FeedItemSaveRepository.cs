using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feed
{
    public class FeedItemSaveRepository(ApplicationDbContext context)
        : GenericRepository<FeedItemSave>(context), IFeedItemSaveRepository
    {
        private readonly DbSet<FeedItemSave> _dbSet = context.Set<FeedItemSave>();

        public async Task<bool> IsAlreadySavedAsync(Guid feedItemId, Guid userId)
        {
            return await _dbSet.AnyAsync(s => s.FeedItemId == feedItemId
                                         && s.UserId == userId
                                         && !s.IsDeleted);
        }

        public IAsyncEnumerable<FeedItemSave> GetSavedItemsByUserIdStream(Guid userId)
        {
            return _dbSet
                .Where(s => s.UserId == userId && !s.IsDeleted)
                .Include(s => s.FeedItem)
                .OrderByDescending(s => s.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task<int> GetSaveCountByFeedItemIdAsync(Guid feedItemId)
        {
            return await _dbSet.CountAsync(s => s.FeedItemId == feedItemId && !s.IsDeleted);
        }

        public async Task UnsaveItemAsync(Guid feedItemId, Guid userId)
        {
            var savedItem = await _dbSet.FirstOrDefaultAsync(s => s.FeedItemId == feedItemId
                                                              && s.UserId == userId);

            if (savedItem is not null)
            {
                _dbSet.Remove(savedItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task<FeedItemSave?> GetByFeedItemAndUserAsync(Guid feedItemId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(
                s => s.FeedItemId == feedItemId &&
                     s.UserId == userId &&
                     !s.IsDeleted,
                cancellationToken);
        }

        public async Task RemoveAsync(FeedItemSave entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
