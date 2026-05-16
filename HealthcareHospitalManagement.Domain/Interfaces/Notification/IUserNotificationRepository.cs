using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Notification;

namespace HealthcareHospitalManagement.Domain.Interfaces.Notification
{
    public interface IUserNotificationRepository : IGenericRepository<UserNotification>
    {
        Task<IEnumerable<UserNotification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken ct = default);

        IAsyncEnumerable<UserNotification> GetUserNotificationsStream(Guid userId, int limit = 50);

        Task MarkAllAsReadAsync(Guid userId, CancellationToken ct = default);

        Task MarkAsReadAsync(Guid notificationId, CancellationToken ct = default);

        Task<IEnumerable<UserNotification>> GetPendingDeliveryAsync(string channel, CancellationToken ct = default);

        Task DeleteExpiredNotificationsAsync(CancellationToken ct = default);

        // ✅ নতুন করে SaveChangesAsync যোগ করা হলো যাতে handler থেকে call করা যায়
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        // 🔹 নির্দিষ্ট ইউজারের নোটিফিকেশন লিস্ট (pagination সহ)
        Task<PagedResponse<UserNotification>> GetUserNotificationsAsync(
            Guid userId,
            bool? isRead,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
