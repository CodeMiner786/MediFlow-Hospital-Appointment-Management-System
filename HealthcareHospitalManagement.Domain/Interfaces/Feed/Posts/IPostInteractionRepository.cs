using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostInteractionRepository : IGenericRepository<PostInteraction>
    {
        // পোস্ট আইডি দিয়ে ইন্টারঅ্যাকশন রেকর্ডটি খুঁজে বের করা
        Task<PostInteraction?> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken);

        // একটি পোস্টের সমস্ত ইন্টারঅ্যাকশন (Likes, Comments, Shares, Saves) একসাথে লোড করা
        Task<PostInteraction?> GetPostStatsWithDetailsAsync(Guid postId, CancellationToken cancellationToken);

        // নির্দিষ্ট পোস্টের শুধুমাত্র লাইক সংখ্যা এবং কমেন্ট সংখ্যা বের করা
        Task<(int LikeCount, int CommentCount, int ShareCount)> GetInteractionCountsAsync(Guid postId, CancellationToken cancellationToken);
    }
}
