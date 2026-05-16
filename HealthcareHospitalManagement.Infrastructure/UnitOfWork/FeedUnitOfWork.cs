using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feed;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class FeedUnitOfWork(ApplicationDbContext context) : IFeedUnitOfWork
    {
        // ——— Feed (Lazy) ———
        private IFeedItemRepository? _feedItems;
        private IFeedItemLikeRepository? _feedItemLikes;
        private IFeedItemSaveRepository? _feedItemSaves;
        private IFeedItemTagRepository? _feedItemTags;

        // ——— Posts (Lazy) ———
        private IPostRepository? _posts;
        private IPostAudienceRepository? _postAudiences;
        private IPostCommentRepository? _postComments;
        private IPostContentRepository? _postContents;
        private IPostInteractionRepository? _postInteractions;
        private IPostLikeRepository? _postLikes;
        private IPostMediaRepository? _postMediaFiles;
        private IPostMetaRepository? _postMetas;
        private IPostSaveRepository? _postSaves;
        private IPostShareRepository? _postShares;
        private IPostTagRepository? _postTags;

        // ——— Feed Properties ———
        public IFeedItemRepository FeedItems
            => _feedItems ??= new FeedItemRepository(context);

        public IFeedItemLikeRepository FeedItemLikes
            => _feedItemLikes ??= new FeedItemLikeRepository(context);

        public IFeedItemSaveRepository FeedItemSaves
            => _feedItemSaves ??= new FeedItemSaveRepository(context);

        public IFeedItemTagRepository FeedItemTags
            => _feedItemTags ??= new FeedItemTagRepository(context);

        // ——— Post Properties ———
        public IPostRepository Posts
            => _posts ??= new PostRepository(context);

        public IPostAudienceRepository PostAudiences
            => _postAudiences ??= new PostAudienceRepository(context);

        public IPostCommentRepository PostComments
            => _postComments ??= new PostCommentRepository(context);

        public IPostContentRepository PostContents
            => _postContents ??= new PostContentRepository(context);

        public IPostInteractionRepository PostInteractions
            => _postInteractions ??= new PostInteractionRepository(context);

        public IPostLikeRepository PostLikes
            => _postLikes ??= new PostLikeRepository(context);

        public IPostMediaRepository PostMediaFiles
            => _postMediaFiles ??= new PostMediaRepository(context);

        public IPostMetaRepository PostMetas
            => _postMetas ??= new PostMetaRepository(context);

        public IPostSaveRepository PostSaves
            => _postSaves ??= new PostSaveRepository(context);

        public IPostShareRepository PostShares
            => _postShares ??= new PostShareRepository(context);

        public IPostTagRepository PostTags
            => _postTags ??= new PostTagRepository(context);

        // ——— Save & Dispose ———
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
