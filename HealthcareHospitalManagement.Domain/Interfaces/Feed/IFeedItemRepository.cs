using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed
{
    public interface IFeedItemRepository : IGenericRepository<FeedItem>
    {
        IAsyncEnumerable<FeedItem> GetFeedByUserIdStream(Guid userId);

        IAsyncEnumerable<FeedItem> GetActiveFeedsByTypeStream(FeedItemType type);

        IAsyncEnumerable<FeedItem> GetFeedsByCityStream(string city);

        IAsyncEnumerable<FeedItem> GetTopRatedFeedsStream(int count);

        Task<FeedItem?> GetFeedWithDetailsAsync(Guid id);

        // Update method (synchronous)
        void Update(FeedItem entity);
    }
}
