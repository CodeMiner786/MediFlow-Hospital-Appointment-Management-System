using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Notification
{
    public class UserNotificationRepository(ApplicationDbContext context)
        : GenericRepository<UserNotification>(context), IUserNotificationRepository
    {
        private readonly DbSet<UserNotification> _dbSet = context.Set<UserNotification>();

        public async Task<IEnumerable<UserNotification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(ct);
        }

        public IAsyncEnumerable<UserNotification> GetUserNotificationsStream(Guid userId, int limit = 50)
        {
            return _dbSet
                .Where(n => n.UserId == userId && !n.IsDeleted)
                .OrderByDescending(n => n.CreatedAt)
                .Take(limit)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task MarkAllAsReadAsync(Guid userId, CancellationToken ct = default)
        {
            var unread = await _dbSet.Where(n => n.UserId == userId && !n.IsRead).ToListAsync(ct);
            foreach (var n in unread)
            {
                n.IsRead = true;
                n.ReadAt = DateTime.UtcNow;
            }
            await context.SaveChangesAsync(ct);
        }

        public async Task MarkAsReadAsync(Guid notificationId, CancellationToken ct = default)
        {
            var notification = await GetByIdAsync(notificationId, ct);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                await context.SaveChangesAsync(ct);
            }
        }

        public async Task<IEnumerable<UserNotification>> GetPendingDeliveryAsync(string channel, CancellationToken ct = default)
        {
            var query = _dbSet.Where(n => !n.IsDeleted);

            query = channel.ToLower() switch
            {
                "email" => query.Where(n => !n.IsEmailSent),
                "sms" => query.Where(n => !n.IsSmsSent),
                "push" => query.Where(n => !n.IsPushSent),
                _ => query
            };

            return await query.ToListAsync(ct);
        }

        public async Task DeleteExpiredNotificationsAsync(CancellationToken ct = default)
        {
            var expired = _dbSet.Where(n => n.ExpiresAt < DateTime.UtcNow);
            _dbSet.RemoveRange(expired);
            await context.SaveChangesAsync(ct);
        }

        // 🔹 Handler থেকে call করার জন্য
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await context.SaveChangesAsync(ct);
        }


        public async Task<PagedResponse<UserNotification>> GetUserNotificationsAsync(
            Guid userId,
            bool? isRead,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(n => n.UserId == userId && !n.IsDeleted);

            if (isRead.HasValue)
            {
                query = query.Where(n => n.IsRead == isRead.Value);
            }

            query = query.OrderByDescending(n => n.CreatedAt);

            return await ToPagedAsync(query, pageNumber, pageSize, cancellationToken);
        }

    }
}
