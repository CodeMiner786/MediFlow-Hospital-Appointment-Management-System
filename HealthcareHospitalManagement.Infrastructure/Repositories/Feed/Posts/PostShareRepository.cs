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
    public class PostShareRepository(ApplicationDbContext context)
        : GenericRepository<PostShare>(context), IPostShareRepository
    {
        private readonly DbSet<PostShare> _dbSet = context.Set<PostShare>();

        // 🔹 নির্দিষ্ট ইন্টারঅ্যাকশন আইডির অধীনে মোট শেয়ার সংখ্যা বের করা
        public async Task<int> GetShareCountByInteractionIdAsync(Guid interactionId)
        {
            return await _dbSet.CountAsync(s => s.PostInteractionId == interactionId && !s.IsDeleted);
        }

        // 🔹 একজন ইউজার কোন কোন পোস্ট শেয়ার করেছেন তা ক্রমানুসারে আনা
        public IAsyncEnumerable<PostShare> GetSharesByUserIdStream(Guid userId)
        {
            return _dbSet
                .Where(s => s.UserId == userId && !s.IsDeleted)
                .Include(s => s.PostInteraction)
                    .ThenInclude(i => i.Post) // শেয়ার করা মূল পোস্টের তথ্য পেতে
                .OrderByDescending(s => s.SharedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 একটি পোস্টের সব শেয়ার এবং কারা শেয়ার করেছে (User Info) তাদের তথ্য দেখা
        public IAsyncEnumerable<PostShare> GetSharesByInteractionIdStream(Guid interactionId)
        {
            return _dbSet
                .Where(s => s.PostInteractionId == interactionId && !s.IsDeleted)
                .Include(s => s.User) // শেয়ারকারীর প্রোফাইল দেখার জন্য
                .OrderByDescending(s => s.SharedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 শেয়ার নোটের ভেতরে কোনো কিওয়ার্ড দিয়ে শেয়ারগুলো সার্চ করা
        public IAsyncEnumerable<PostShare> SearchSharesByNoteStream(string keyword)
        {
            return _dbSet
                .Where(s => s.ShareNote != null &&
                            s.ShareNote.Contains(keyword) &&
                            !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
