using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostTagRepository : IGenericRepository<PostTag>
    {
        // একটি নির্দিষ্ট পোস্টের সাথে যুক্ত সব ট্যাগ স্ট্রিম করা
        IAsyncEnumerable<PostTag> GetTagsByPostIdStream(Guid postId);

        // নির্দিষ্ট একটি ট্যাগ নাম দিয়ে সব পোস্টগুলো খুঁজে বের করা (Search Feature)
        IAsyncEnumerable<PostTag> GetPostsByTagNameStream(string tagName);

        // সবচেয়ে বেশি ব্যবহৃত ট্যাগগুলো খুঁজে বের করা (Trending/Popular Tags)
        Task<IEnumerable<string>> GetPopularTagsAsync(int count);

        // পোস্ট থেকে নির্দিষ্ট একটি ট্যাগ মুছে ফেলা
        Task RemoveTagFromPostAsync(Guid postId, string tagName);
    }
}
