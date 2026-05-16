using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Notification
{
    public class NotificationTemplateRepository(ApplicationDbContext context)
        : GenericRepository<NotificationTemplate>(context), INotificationTemplateRepository
    {
        private readonly DbSet<NotificationTemplate> _dbSet = context.Set<NotificationTemplate>();

        // 🔹 ইউনিক কোড দিয়ে টেমপ্লেট খুঁজে বের করা
        public async Task<NotificationTemplate?> GetByCodeAsync(string templateCode)
        {
            return await _dbSet.FirstOrDefaultAsync(t =>
                t.TemplateCode == templateCode &&
                t.IsActive &&
                !t.IsDeleted);
        }

        // 🔹 নির্দিষ্ট চ্যানেলের জন্য টেমপ্লেট লিস্ট (pagination সহ)
        public async Task<PagedResponse<NotificationTemplate>> GetByChannelAsync(
            NotificationChannel channel,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(t => t.Channel == channel && t.IsActive && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }

        // 🔹 ক্যাটাগরি ভিত্তিক কুয়েরি (pagination সহ)
        public async Task<PagedResponse<NotificationTemplate>> GetByCategoryAsync(
            NotificationCategory category,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(t => t.Category == category && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }

        // 🔹 ভ্যালিডেশন: একই কোড দিয়ে যেন দুটি টেমপ্লেট না থাকে
        public async Task<bool> IsTemplateCodeUniqueAsync(string templateCode)
        {
            return !await _dbSet.AnyAsync(t => t.TemplateCode == templateCode && !t.IsDeleted);
        }

        // 🔹 অ্যাডমিন প্যানেলে টেমপ্লেট খোঁজার জন্য সার্চ মেথড (pagination সহ)
        public async Task<PagedResponse<NotificationTemplate>> SearchTemplatesAsync(
            string searchTerm,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(t => (t.TemplateCode.Contains(searchTerm) || t.Name.Contains(searchTerm))
                            && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }

        // 🔹 সব টেমপ্লেট paged আকারে আনার জন্য fallback method
        public async Task<PagedResponse<NotificationTemplate>> GetAllPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }
    }
}
