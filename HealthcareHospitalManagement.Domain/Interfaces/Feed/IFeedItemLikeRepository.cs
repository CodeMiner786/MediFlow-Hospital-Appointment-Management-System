using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed
{
    public interface IFeedItemLikeRepository : IGenericRepository<FeedItemLike>
    {
        Task<int> GetLikeCountByFeedItemIdAsync(Guid feedItemId);

        Task<bool> IsUserLikedAsync(Guid feedItemId, Guid userId);

        IAsyncEnumerable<FeedItemLike> GetLikesByFeedItemIdStream(Guid feedItemId);

        IAsyncEnumerable<FeedItemLike> GetUserLikedItemsStream(Guid userId);

        Task RemoveLikeAsync(Guid feedItemId, Guid userId);

        // নতুন method: নির্দিষ্ট ইউজার + ফিড আইটেম entity ফেরত আনা
        Task<FeedItemLike?> GetByFeedItemAndUserAsync(Guid feedItemId, Guid userId, CancellationToken cancellationToken = default);

        // entity remove করার জন্য clean async method
        Task RemoveAsync(FeedItemLike entity, CancellationToken cancellationToken = default);
    }
}
