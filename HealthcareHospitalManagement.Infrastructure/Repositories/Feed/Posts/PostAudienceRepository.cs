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
    public class PostAudienceRepository(ApplicationDbContext context)
        : GenericRepository<PostAudience>(context), IPostAudienceRepository
    {
        private readonly DbSet<PostAudience> _dbSet = context.Set<PostAudience>();

        // 🔹 পোস্ট আইডি দিয়ে অডিয়েন্স সেটিংস এবং পোস্টের তথ্য একসাথে আনা
        public async Task<PostAudience?> GetByPostIdAsync(Guid postId)
        {
            return await _dbSet
                .Include(a => a.Post) // পোস্টের মেইন ডেটা দেখার জন্য Include
                .FirstOrDefaultAsync(a => a.PostId == postId && !a.IsDeleted);
        }

        // 🔹 ভিজিবিলিটি (Public, Followers, Private) অনুযায়ী ফিল্টার করা
        public IAsyncEnumerable<PostAudience> GetByVisibilityStream(PostVisibility visibility)
        {
            return _dbSet
                .Where(a => a.Visibility == visibility && !a.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট শহরের ইউজারদের জন্য টার্গেট করা পোস্টগুলো খুঁজে বের করা
        public IAsyncEnumerable<PostAudience> GetByTargetCityStream(string city)
        {
            return _dbSet
                .Where(a => a.TargetCity != null &&
                            a.TargetCity.ToLower() == city.ToLower() &&
                            !a.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 যে পোস্টগুলো পরিচয় গোপন (Anonymous) রেখে করা হয়েছে সেগুলো বের করা
        public IAsyncEnumerable<PostAudience> GetAnonymousPostAudiencesStream()
        {
            return _dbSet
                .Where(a => a.IsAnonymous && !a.IsDeleted)
                .Include(a => a.Post)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
