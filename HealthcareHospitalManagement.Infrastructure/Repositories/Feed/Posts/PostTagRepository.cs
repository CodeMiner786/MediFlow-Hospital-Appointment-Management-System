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
    public class PostTagRepository(ApplicationDbContext context)
        : GenericRepository<PostTag>(context), IPostTagRepository
    {
        private readonly DbSet<PostTag> _dbSet = context.Set<PostTag>();

        // 🔹 পোস্ট আইডি দিয়ে ওই পোস্টের সব ট্যাগ দেখা
        public IAsyncEnumerable<PostTag> GetTagsByPostIdStream(Guid postId)
        {
            return _dbSet
                .Where(t => t.PostId == postId && !t.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 ট্যাগ নাম দিয়ে সার্চ করে সংশ্লিষ্ট পোস্টগুলো এবং অথর ইনফো লোড করা
        public IAsyncEnumerable<PostTag> GetPostsByTagNameStream(string tagName)
        {
            return _dbSet
                .Where(t => t.Tag.ToLower() == tagName.ToLower() && !t.IsDeleted)
                .Include(t => t.Post)
                    .ThenInclude(p => p.AuthorUser) // পোস্টের সাথে রাইটারের নাম দেখানোর জন্য
                .OrderByDescending(t => t.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 ট্রেন্ডিং ট্যাগ বের করার লজিক (সবচেয়ে বেশি কোন ট্যাগ ব্যবহার হয়েছে)
        public async Task<IEnumerable<string>> GetPopularTagsAsync(int count)
        {
            return await _dbSet
                .Where(t => !t.IsDeleted)
                .GroupBy(t => t.Tag)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(count)
                .ToListAsync();
        }

        // 🔹 নির্দিষ্ট পোস্ট থেকে ট্যাগ রিমুভ করা
        public async Task RemoveTagFromPostAsync(Guid postId, string tagName)
        {
            var tagEntry = await _dbSet.FirstOrDefaultAsync(t =>
                t.PostId == postId &&
                t.Tag.ToLower() == tagName.ToLower());

            if (tagEntry != null)
            {
                _dbSet.Remove(tagEntry);
                await context.SaveChangesAsync();
            }
        }
    }
}
