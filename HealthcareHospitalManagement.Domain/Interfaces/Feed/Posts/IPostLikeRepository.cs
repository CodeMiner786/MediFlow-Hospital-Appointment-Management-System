using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostLikeRepository : IGenericRepository<PostLike>
    {
        // ইউজার নির্দিষ্ট ইন্টারঅ্যাকশনে (পোস্টে) অলরেডি লাইক দিয়েছে কি না চেক করা
        Task<bool> IsAlreadyLikedAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken);

        // নির্দিষ্ট একটি ইন্টারঅ্যাকশনের সব লাইক এবং দাতা ইউজারদের তথ্য আনা
        IAsyncEnumerable<PostLike> GetLikesByInteractionIdStream(Guid interactionId);

        // একজন ইউজার কোন কোন পোস্টে লাইক দিয়েছে তার লিস্ট (Async Stream)
        IAsyncEnumerable<PostLike> GetUserLikedPostsStream(Guid userId, CancellationToken cancellationToken);

        // লাইক রিমুভ বা আন-লাইক (Unlike) করা
        Task RemoveLikeAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken);

        Task<PostLike?> GetByInteractionAndUserAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken);
    }
}
