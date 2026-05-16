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
    public class PostSaveRepository(ApplicationDbContext context)
        : GenericRepository<PostSave>(context), IPostSaveRepository
    {
        private readonly DbSet<PostSave> _dbSet = context.Set<PostSave>();

        // 🔹 ডুপ্লিকেট সেভ চেক করা (একই ইউজার যেন এক পোস্ট বারবার সেভ না করে)
        public async Task<bool> IsPostAlreadySavedAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(s => s.PostInteractionId == interactionId
                                         && s.UserId == userId
                                         && !s.IsDeleted);
        }

        // 🔹 ইউজারের সেভ করা সব পোস্টের লিস্ট এবং পোস্টের মেইন ডাটা লোড করা
        public IAsyncEnumerable<PostSave> GetSavedPostsByUserIdStream(Guid userId)
        {
            return _dbSet
                .Where(s => s.UserId == userId && !s.IsDeleted)
                .Include(s => s.PostInteraction)
                    .ThenInclude(i => i.Post) // সেভ করা পোস্টের টাইটেল/কন্টেন্ট দেখার জন্য
                .OrderByDescending(s => s.SavedAt) // লেটেস্ট সেভগুলো আগে থাকবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 নির্দিষ্ট পোস্টের সেভ সংখ্যা গণনা করা
        public async Task<int> GetSaveCountByInteractionIdAsync(Guid interactionId, CancellationToken cancellationToken)
        {
            return await _dbSet.CountAsync(s => s.PostInteractionId == interactionId && !s.IsDeleted, cancellationToken);
        }

        // 🔹 আন-সেভ (Unsave) বা বুকমার্ক রিমুভ করা
        public async Task UnsavePostAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken)
        {
            var savedItem = await _dbSet.FirstOrDefaultAsync(s => s.PostInteractionId == interactionId
                                                              && s.UserId == userId, cancellationToken);
            if (savedItem != null)
            {
                _dbSet.Remove(savedItem); // সাধারণত বুকমার্ক হার্ড ডিলিট করা হয়
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<PostSave?> GetByInteractionAndUserAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(
                s => s.PostInteractionId == interactionId
                  && s.UserId == userId
                  && !s.IsDeleted,
                cancellationToken);
        }

    }
}
