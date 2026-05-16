using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        // নির্দিষ্ট ইউজারের সব পোস্ট টাইমলাইন অনুযায়ী দেখা
        IAsyncEnumerable<Post> GetPostsByAuthorIdStream(Guid authorUserId);

        // নির্দিষ্ট ফিড আইটেমের সাথে যুক্ত পোস্টগুলো বের করা (যদি থাকে)
        IAsyncEnumerable<Post> GetPostsByLinkedFeedItemStream(Guid feedItemId);

        // পোস্টের সব ডিটেইলস (Media, Tags, Author, Interactions) সহ একটি পোস্ট আনা
        Task<Post?> GetPostWithFullDetailsAsync(Guid postId);

        // ট্রেন্ডিং বা লেটেস্ট পাবলিক পোস্টগুলো খুঁজে বের করা
        IAsyncEnumerable<Post> GetLatestPublicPostsStream(int count);

        // নির্দিষ্ট ট্যাগ অনুযায়ী পোস্ট সার্চ করা
        IAsyncEnumerable<Post> GetPostsByTagStream(string tagName);

        // 🔹 Post এর সাথে details (Meta, Audience, Tags, MediaFiles) লোড করা
        Task<Post?> GetByIdWithDetailsAsync(Guid postId, CancellationToken cancellationToken);

        // 🔹 Post update করার জন্য
        void Update(Post post);

        // ✅ Pagination method (enum + filter সহ)
        Task<PagedResponse<Post>> GetPagedAsync(
            PostType? postType,
            PostVisibility? visibility,
            PostStatus? status,
            string? tag,
            string? keyword,
            Guid? authorId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
