using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed
{
    public interface IFeedItemSaveRepository : IGenericRepository<FeedItemSave>
    {
        Task<bool> IsAlreadySavedAsync(Guid feedItemId, Guid userId);

        IAsyncEnumerable<FeedItemSave> GetSavedItemsByUserIdStream(Guid userId);

        Task<int> GetSaveCountByFeedItemIdAsync(Guid feedItemId);

        Task UnsaveItemAsync(Guid feedItemId, Guid userId);

        // নতুন method: নির্দিষ্ট ইউজার + ফিড আইটেম entity ফেরত আনা
        Task<FeedItemSave?> GetByFeedItemAndUserAsync(Guid feedItemId, Guid userId, CancellationToken cancellationToken = default);

        // নতুন method: entity remove করার জন্য
        Task RemoveAsync(FeedItemSave entity, CancellationToken cancellationToken = default);
    }

}
