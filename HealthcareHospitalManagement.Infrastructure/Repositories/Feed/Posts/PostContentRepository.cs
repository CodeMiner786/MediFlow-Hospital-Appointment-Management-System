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
    public class PostContentRepository(ApplicationDbContext context)
        : GenericRepository<PostContent>(context), IPostContentRepository
    {
        private readonly DbSet<PostContent> _dbSet = context.Set<PostContent>();

        // 🔹 পোস্ট আইডি দিয়ে কন্টেন্ট এবং পোস্টের মেইন অবজেক্ট একসাথে আনা
        public async Task<PostContent?> GetByPostIdAsync(Guid postId)
        {
            return await _dbSet
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.PostId == postId && !c.IsDeleted);
        }

        // 🔹 পোস্টের ধরন (Type) অনুযায়ী ফিল্টার করে ডেটা স্ট্রিম করা
        public IAsyncEnumerable<PostContent> GetByPostTypeStream(PostType type)
        {
            return _dbSet
                .Where(c => c.PostType == type && !c.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট ভাষা অনুযায়ী কন্টেন্ট খুঁজে বের করা
        public IAsyncEnumerable<PostContent> GetByLanguageStream(string language)
        {
            return _dbSet
                .Where(c => c.Language != null &&
                            c.Language.ToLower() == language.ToLower() &&
                            !c.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 টেক্সট বডির ভেতরে কোন নির্দিষ্ট শব্দ আছে কি না তা সার্চ করা
        public IAsyncEnumerable<PostContent> SearchByKeywordStream(string keyword)
        {
            return _dbSet
                .Where(c => c.TextBody != null &&
                            c.TextBody.Contains(keyword) &&
                            !c.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
