using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
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
    public class PostMetaRepository(ApplicationDbContext context)
        : GenericRepository<PostMeta>(context), IPostMetaRepository
    {
        private readonly DbSet<PostMeta> _dbSet = context.Set<PostMeta>();

        // 🔹 পোস্ট আইডি দিয়ে মেটা ইনফরমেশন এবং ট্যাগগুলো একসাথে আনা
        public async Task<PostMeta?> GetByPostIdAsync(Guid postId)
        {
            return await _dbSet
                .Include(m => m.Tags) // মেটার সাথে যুক্ত ট্যাগ লোড করা
                .FirstOrDefaultAsync(m => m.PostId == postId && !m.IsDeleted);
        }

        // 🔹 স্ট্যাটাস অনুযায়ী পোস্ট ফিল্টার করা (যেমন: পাবলিশড পোস্টগুলো আগে দেখানো)
        public IAsyncEnumerable<PostMeta> GetByStatusStream(PostStatus status)
        {
            return _dbSet
                .Where(m => m.Status == status && !m.IsDeleted)
                .OrderByDescending(m => m.PublishedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 পিন করা পোস্টগুলো খুঁজে বের করা
        public IAsyncEnumerable<PostMeta> GetPinnedPostsStream()
        {
            return _dbSet
                .Where(m => m.IsPinned && !m.IsDeleted)
                .Include(m => m.Post)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট সময়ের মধ্যে পাবলিশ হওয়া পোস্টগুলো বের করা (রিপোর্টের জন্য সুবিধাজনক)
        public IAsyncEnumerable<PostMeta> GetPublishedInDateRangeStream(DateTime start, DateTime end)
        {
            return _dbSet
                .Where(m => m.PublishedAt >= start && m.PublishedAt <= end && !m.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 পোস্টের স্ট্যাটাস এবং পাবলিশ টাইম আপডেট করা
        public async Task UpdateStatusAsync(Guid postId, PostStatus newStatus)
        {
            var meta = await _dbSet.FirstOrDefaultAsync(m => m.PostId == postId);
            if (meta != null)
            {
                meta.Status = newStatus;

                // যদি স্ট্যাটাস Published হয়, তবে বর্তমান সময় সেট করা
                if (newStatus == PostStatus.Published)
                    meta.PublishedAt = DateTime.UtcNow;

                _dbSet.Update(meta);
                await context.SaveChangesAsync();
            }
        }
    }
}
