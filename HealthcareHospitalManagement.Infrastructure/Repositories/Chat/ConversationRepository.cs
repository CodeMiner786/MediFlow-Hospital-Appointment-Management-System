using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Interfaces.Chat;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Chat
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে পাস করা হয়েছে
    public class ConversationRepository(ApplicationDbContext context)
        : GenericRepository<Conversation>(context), IConversationRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<Conversation> _dbSet = context.Set<Conversation>();

        // ইউজার ParticipantA অথবা ParticipantB যাই হোক না কেন, তার সব কনভারসেশন স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<Conversation> GetUserConversationsStream(Guid userId)
        {
            return _dbSet
                .Where(c => (c.ParticipantAId == userId || c.ParticipantBId == userId)
                         && c.IsActive && !c.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // দুজন ইউজারের ইউনিক পেয়ার চেক করে কনভারসেশন খুঁজে বের করা (যাতে ডুপ্লিকেট চ্যাট তৈরি না হয়)
        public async Task<Conversation?> GetConversationBetweenUsersAsync(Guid userIdA, Guid userIdB)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c =>
                    ((c.ParticipantAId == userIdA && c.ParticipantBId == userIdB) ||
                     (c.ParticipantAId == userIdB && c.ParticipantBId == userIdA))
                    && !c.IsDeleted);
        }

        // রেফারেন্স আইডি (যেমন: AppointmentId) দিয়ে কনভারসেশন খুঁজে বের করা
        public async Task<Conversation?> GetByReferenceIdAsync(Guid referenceId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.ReferenceId == referenceId && !c.IsDeleted);
        }

        // চ্যাট লিস্ট দেখানোর জন্য লেটেস্ট মেসেজের সময় অনুযায়ী সাজিয়ে ডেটা পাঠানো হচ্ছে
        public IAsyncEnumerable<Conversation> GetRecentConversationsStream(Guid userId)
        {
            return _dbSet
                .Where(c => (c.ParticipantAId == userId || c.ParticipantBId == userId) && !c.IsDeleted)
                .OrderByDescending(c => c.LastMessageAt) // লেটেস্ট চ্যাট আগে দেখাবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
