using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostCommentRepository : IGenericRepository<PostComment>
    {
        // 🔹 নির্দিষ্ট ইন্টারঅ্যাকশন আইডি দিয়ে সব মেইন কমেন্টগুলো আনা (রিপ্লাই বাদে)
        IAsyncEnumerable<PostComment> GetMainCommentsByInteractionIdStream(Guid interactionId);

        // 🔹 একটি নির্দিষ্ট কমেন্টের অধীনে কতগুলো রিপ্লাই আছে তা দেখা
        IAsyncEnumerable<PostComment> GetRepliesByCommentIdStream(Guid parentCommentId);

        // 🔹 নির্দিষ্ট ইউজারের করা সমস্ত কমেন্ট খুঁজে বের করা
        IAsyncEnumerable<PostComment> GetCommentsByUserIdStream(Guid userId);

        // 🔹 কমেন্টের সাথে ইউজার এবং রিপ্লাইসহ ডিটেইলস আনা
        Task<PostComment?> GetCommentWithRepliesAsync(Guid commentId);

        // 🔹 একটি পোস্ট বা ইন্টারঅ্যাকশনে মোট কতটি কমেন্ট আছে তার সংখ্যা
        Task<int> GetTotalCommentCountAsync(Guid interactionId);

        // 🔹 কমেন্টের সাথে ইউজার লোড করা
        Task<PostComment?> GetByIdWithUserAsync(Guid commentId, CancellationToken cancellationToken);

        // ✅ Optional: Pagination method (যদি সরাসরি repository থেকে call করতে চাও)
        Task<PagedResponse<PostComment>> GetPagedByPostIdAsync(
            Guid postId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
