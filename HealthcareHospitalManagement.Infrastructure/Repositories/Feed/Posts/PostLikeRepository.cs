using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feed.Posts
{
    public class PostLikeRepository(ApplicationDbContext context)
        : GenericRepository<PostLike>(context), IPostLikeRepository
    {
        private readonly DbSet<PostLike> _dbSet = context.Set<PostLike>();

        // 🔹 ইউজার নির্দিষ্ট ইন্টারঅ্যাকশনে (পোস্টে) অলরেডি লাইক দিয়েছে কি না চেক করা
        public async Task<bool> IsAlreadyLikedAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(
                l => l.PostInteractionId == interactionId
                  && l.UserId == userId
                  && !l.IsDeleted,
                cancellationToken);
        }

        // 🔹 নির্দিষ্ট একটি ইন্টারঅ্যাকশনের সব লাইক এবং দাতা ইউজারদের তথ্য আনা
        public IAsyncEnumerable<PostLike> GetLikesByInteractionIdStream(Guid interactionId)
        {
            return _dbSet
                .Where(l => l.PostInteractionId == interactionId && !l.IsDeleted)
                .Include(l => l.User) // ইউজারের তথ্য লোড করা
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 একজন ইউজার কোন কোন পোস্টে লাইক দিয়েছে তার লিস্ট (Async Stream)
        public IAsyncEnumerable<PostLike> GetUserLikedPostsStream(Guid userId, CancellationToken cancellationToken)
        {
            return _dbSet
                .Where(l => l.UserId == userId && !l.IsDeleted)
                .Include(l => l.PostInteraction)
                    .ThenInclude(pi => pi.Post) // পোস্টের ডাটা লোড করা
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 লাইক রিমুভ বা আন-লাইক (Unlike) করা
        public async Task RemoveLikeAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken)
        {
            var like = await _dbSet.FirstOrDefaultAsync(
                l => l.PostInteractionId == interactionId
                  && l.UserId == userId,
                cancellationToken);

            if (like != null)
            {
                _dbSet.Remove(like);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        // 🔹 নির্দিষ্ট interaction + user এর লাইক রেকর্ড বের করা
        public async Task<PostLike?> GetByInteractionAndUserAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(
                l => l.PostInteractionId == interactionId
                  && l.UserId == userId
                  && !l.IsDeleted,
                cancellationToken);
        }
    }
}
