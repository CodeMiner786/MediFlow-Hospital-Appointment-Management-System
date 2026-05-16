using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feed.Posts
{
    public class PostCommentRepository(ApplicationDbContext context)
        : GenericRepository<PostComment>(context), IPostCommentRepository
    {
        private readonly DbSet<PostComment> _dbSet = context.Set<PostComment>();

        public IAsyncEnumerable<PostComment> GetMainCommentsByInteractionIdStream(Guid interactionId)
        {
            return _dbSet
                .Where(c => c.PostInteractionId == interactionId && c.ParentCommentId == null && !c.IsDeleted)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<PostComment> GetRepliesByCommentIdStream(Guid parentCommentId)
        {
            return _dbSet
                .Where(c => c.ParentCommentId == parentCommentId && !c.IsDeleted)
                .Include(c => c.User)
                .OrderBy(c => c.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<PostComment> GetCommentsByUserIdStream(Guid userId)
        {
            return _dbSet
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .Include(c => c.PostInteraction)
                .OrderByDescending(c => c.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task<PostComment?> GetCommentWithRepliesAsync(Guid commentId)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted);
        }

        public async Task<int> GetTotalCommentCountAsync(Guid interactionId)
        {
            return await _dbSet.CountAsync(c => c.PostInteractionId == interactionId && !c.IsDeleted);
        }

        public async Task<PostComment?> GetByIdWithUserAsync(Guid commentId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted, cancellationToken);
        }

        // ✅ Pagination method (entity অনুযায়ী আপডেট করা হয়েছে)
        public async Task<PagedResponse<PostComment>> GetPagedByPostIdAsync(
            Guid interactionId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(c => c.PostInteractionId == interactionId && !c.IsDeleted)
                .Include(c => c.User)
                .Include(c => c.Replies)
                .OrderByDescending(c => c.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }
    }
}
