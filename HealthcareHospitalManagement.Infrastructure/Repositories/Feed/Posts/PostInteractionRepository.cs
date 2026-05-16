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
    public class PostInteractionRepository(ApplicationDbContext context)
        : GenericRepository<PostInteraction>(context), IPostInteractionRepository
    {
        private readonly DbSet<PostInteraction> _dbSet = context.Set<PostInteraction>();

        // 🔹 পোস্ট আইডি দিয়ে বেসিক ইন্টারঅ্যাকশন ডেটা আনা
        public async Task<PostInteraction?> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .FirstOrDefaultAsync(i => i.PostId == postId && !i.IsDeleted);
        }

        // 🔹 একটি পোস্টের যাবতীয় এনগেজমেন্ট ডিটেইলস (Eager Loading)
        public async Task<PostInteraction?> GetPostStatsWithDetailsAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(i => i.Likes.Where(l => !l.IsDeleted))
                .Include(i => i.Comments.Where(c => !c.IsDeleted))
                .Include(i => i.Shares.Where(s => !s.IsDeleted))
                .Include(i => i.Saves.Where(sv => !sv.IsDeleted))
                .FirstOrDefaultAsync(i => i.PostId == postId && !i.IsDeleted, cancellationToken);
        }

        // 🔹 পারফরম্যান্স অপ্টিমাইজড উপায়ে শুধুমাত্র সংখ্যাগুলো (Counts) বের করা
        public async Task<(int LikeCount, int CommentCount, int ShareCount)> GetInteractionCountsAsync(Guid postId, CancellationToken cancellationToken)
        {
            var interaction = await _dbSet
                .Where(i => i.PostId == postId && !i.IsDeleted)
                .Select(i => new
                {
                    LikeCount = i.Likes.Count(l => !l.IsDeleted),
                    CommentCount = i.Comments.Count(c => !c.IsDeleted),
                    ShareCount = i.Shares.Count(s => !s.IsDeleted)
                })
                .FirstOrDefaultAsync(cancellationToken);

            return interaction != null
                ? (interaction.LikeCount, interaction.CommentCount, interaction.ShareCount)
                : (0, 0, 0);
        }
    }
}
