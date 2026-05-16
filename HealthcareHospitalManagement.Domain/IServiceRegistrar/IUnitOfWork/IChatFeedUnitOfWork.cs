using HealthcareHospitalManagement.Domain.Interfaces.Chat;
using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IChatFeedUnitOfWork : IDisposable
    {
        // Chat
        IConversationRepository Conversations { get; }
        IChatMessageRepository ChatMessages { get; }

        // Feed
        IFeedItemRepository FeedItems { get; }
        IFeedItemLikeRepository FeedItemLikes { get; }
        IFeedItemSaveRepository FeedItemSaves { get; }
        IFeedItemTagRepository FeedItemTags { get; }

        // Posts
        IPostRepository Posts { get; }
        IPostAudienceRepository PostAudiences { get; }
        IPostCommentRepository PostComments { get; }
        IPostContentRepository PostContents { get; }
        IPostInteractionRepository PostInteractions { get; }
        IPostLikeRepository PostLikes { get; }
        IPostMediaRepository PostMedias { get; }
        IPostMetaRepository PostMetas { get; }
        IPostSaveRepository PostSaves { get; }
        IPostShareRepository PostShares { get; }
        IPostTagRepository PostTags { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
