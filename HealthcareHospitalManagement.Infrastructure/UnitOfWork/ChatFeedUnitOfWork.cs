using HealthcareHospitalManagement.Domain.Interfaces.Chat;
using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Chat;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feed;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feed.Posts;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class ChatFeedUnitOfWork(ApplicationDbContext context) : IChatFeedUnitOfWork
    {
        // Chat
        public IConversationRepository Conversations { get; } = new ConversationRepository(context);
        public IChatMessageRepository ChatMessages { get; } = new ChatMessageRepository(context);

        // Feed
        public IFeedItemRepository FeedItems { get; } = new FeedItemRepository(context);
        public IFeedItemLikeRepository FeedItemLikes { get; } = new FeedItemLikeRepository(context);
        public IFeedItemSaveRepository FeedItemSaves { get; } = new FeedItemSaveRepository(context);
        public IFeedItemTagRepository FeedItemTags { get; } = new FeedItemTagRepository(context);

        // Posts
        public IPostRepository Posts { get; } = new PostRepository(context);
        public IPostAudienceRepository PostAudiences { get; } = new PostAudienceRepository(context);
        public IPostCommentRepository PostComments { get; } = new PostCommentRepository(context);
        public IPostContentRepository PostContents { get; } = new PostContentRepository(context);
        public IPostInteractionRepository PostInteractions { get; } = new PostInteractionRepository(context);
        public IPostLikeRepository PostLikes { get; } = new PostLikeRepository(context);
        public IPostMediaRepository PostMedias { get; } = new PostMediaRepository(context);
        public IPostMetaRepository PostMetas { get; } = new PostMetaRepository(context);
        public IPostSaveRepository PostSaves { get; } = new PostSaveRepository(context);
        public IPostShareRepository PostShares { get; } = new PostShareRepository(context);
        public IPostTagRepository PostTags { get; } = new PostTagRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
