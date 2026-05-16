using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed
{
    public interface IFeedItemTagRepository : IGenericRepository<FeedItemTag>
    {
        // একটি নির্দিষ্ট ফিড আইটেমের সব ট্যাগ খুঁজে বের করা
        IAsyncEnumerable<FeedItemTag> GetTagsByFeedItemIdStream(Guid feedItemId);

        // নির্দিষ্ট একটি ট্যাগ (যেমন: #Health) কোন কোন ফিড আইটেমে আছে তা বের করা
        IAsyncEnumerable<FeedItemTag> GetFeedItemsByTagNameStream(string tagName);

        // সবচেয়ে জনপ্রিয় বা বেশি ব্যবহৃত ট্যাগগুলো লিস্ট করা (Trending Tags)
        Task<IEnumerable<string>> GetTrendingTagsAsync(int count);

        // একটি ফিড আইটেম থেকে নির্দিষ্ট একটি ট্যাগ রিমুভ করা
        Task RemoveTagAsync(Guid feedItemId, string tagName);
    }
}
