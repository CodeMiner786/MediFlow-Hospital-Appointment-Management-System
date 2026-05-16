using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feed.Posts
{
    public class PostRepository(ApplicationDbContext context)
        : GenericRepository<Post>(context), IPostRepository
    {
        private readonly DbSet<Post> _dbSet = context.Set<Post>();

        public IAsyncEnumerable<Post> GetPostsByAuthorIdStream(Guid authorUserId)
        {
            return _dbSet
                .Where(p => p.AuthorUserId == authorUserId && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<Post> GetPostsByLinkedFeedItemStream(Guid feedItemId)
        {
            return _dbSet
                .Where(p => p.LinkedFeedItemId == feedItemId && !p.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task<Post?> GetPostWithFullDetailsAsync(Guid postId)
        {
            return await _dbSet
                .Include(p => p.AuthorUser)
                .Include(p => p.MediaFiles)
                .Include(p => p.Tags)
                .Include(p => p.Interactions)
                .Include(p => p.LinkedFeedItem)
                .Include(p => p.Meta)
                .Include(p => p.Audience)
                .Include(p => p.Content)
                .FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted);
        }

        public IAsyncEnumerable<Post> GetLatestPublicPostsStream(int count)
        {
            return _dbSet
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<Post> GetPostsByTagStream(string tagName)
        {
            return _dbSet
                .Where(p => !p.IsDeleted && p.Tags.Any(t => string.Equals(t.Tag, tagName, StringComparison.OrdinalIgnoreCase)))
                .Include(p => p.AuthorUser)
                .OrderByDescending(p => p.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task<Post?> GetByIdWithDetailsAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(p => p.Meta)
                .Include(p => p.Audience)
                .Include(p => p.Content)
                .Include(p => p.Tags)
                .Include(p => p.MediaFiles)
                .FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted, cancellationToken);
        }

        public void Update(Post post)
        {
            _dbSet.Update(post);
        }

        // ✅ Pagination method (enum + TextBody ব্যবহার করে)
        public async Task<PagedResponse<Post>> GetPagedAsync(
            PostType? postType,
            PostVisibility? visibility,
            PostStatus? status,
            string? tag,
            string? keyword,
            Guid? authorId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(p =>
                    !p.IsDeleted &&
                    (postType == null || p.Content.PostType == postType) &&
                    (visibility == null || p.Audience.Visibility == visibility) &&
                    (status == null || p.Meta.Status == status) &&
                    (string.IsNullOrEmpty(tag) || p.Tags.Any(t => string.Equals(t.Tag, tag, StringComparison.OrdinalIgnoreCase))) &&
                    (string.IsNullOrEmpty(keyword) || (p.Content.TextBody != null && p.Content.TextBody.Contains(keyword))) &&
                    (authorId == null || p.AuthorUserId == authorId)
                )
                .Include(p => p.AuthorUser)
                .Include(p => p.Tags)
                .Include(p => p.Meta)
                .Include(p => p.Audience)
                .Include(p => p.Content)
                .OrderByDescending(p => p.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }
    }
}
