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
    public class PostMediaRepository(ApplicationDbContext context)
        : GenericRepository<PostMedia>(context), IPostMediaRepository
    {
        private readonly DbSet<PostMedia> _dbSet = context.Set<PostMedia>();

        // 🔹 পোস্ট আইডি দিয়ে মিডিয়াগুলো ক্রমানুসারে (SortOrder) স্ট্রিম করা
        public IAsyncEnumerable<PostMedia> GetMediaByPostIdStream(Guid postId)
        {
            return _dbSet
                .Where(m => m.PostId == postId && !m.IsDeleted)
                .OrderBy(m => m.SortOrder) // ১, ২, ৩ এভাবে সিরিয়াল বজায় থাকবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট টাইপের মিডিয়া (Image/Video) খুঁজে বের করা
        public IAsyncEnumerable<PostMedia> GetMediaByTypeStream(PostMediaType type)
        {
            return _dbSet
                .Where(m => m.MediaType == type && !m.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 মিডিয়ার ডিসপ্লে অর্ডার পরিবর্তন করা (যেমন: ড্র্যাগ এন্ড ড্রপ ফিচারের জন্য)
        public async Task UpdateMediaSortOrderAsync(Guid mediaId, int newOrder)
        {
            var media = await _dbSet.FindAsync(mediaId);
            if (media != null)
            {
                media.SortOrder = newOrder;
                _dbSet.Update(media);
                await context.SaveChangesAsync();
            }
        }

        // 🔹 একটি পোস্ট ডিলিট করার সময় তার সব মিডিয়া রেফারেন্স ডিলিট করা
        public async Task DeleteAllMediaByPostIdAsync(Guid postId)
        {
            var mediaList = await _dbSet.Where(m => m.PostId == postId).ToListAsync();
            if (mediaList.Any())
            {
                _dbSet.RemoveRange(mediaList);
                await context.SaveChangesAsync();
            }
        }
    }
}
