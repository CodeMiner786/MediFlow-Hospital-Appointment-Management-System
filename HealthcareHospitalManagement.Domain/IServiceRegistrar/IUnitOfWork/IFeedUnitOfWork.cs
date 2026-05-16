using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IFeedUnitOfWork : IDisposable
    {
        // ——— Feed Repositories ———
        IFeedItemRepository FeedItems { get; }
        IFeedItemLikeRepository FeedItemLikes { get; }
        IFeedItemSaveRepository FeedItemSaves { get; }
        IFeedItemTagRepository FeedItemTags { get; }

        // ——— Post Repositories ———
        IPostRepository Posts { get; }
        IPostAudienceRepository PostAudiences { get; }
        IPostCommentRepository PostComments { get; }
        IPostContentRepository PostContents { get; }
        IPostInteractionRepository PostInteractions { get; }
        IPostLikeRepository PostLikes { get; }
        IPostMediaRepository PostMediaFiles { get; }
        IPostMetaRepository PostMetas { get; }
        IPostSaveRepository PostSaves { get; }
        IPostShareRepository PostShares { get; }
        IPostTagRepository PostTags { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
