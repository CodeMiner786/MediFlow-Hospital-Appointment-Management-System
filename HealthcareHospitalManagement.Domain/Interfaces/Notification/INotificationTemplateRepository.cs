using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Enums.Notification;

namespace HealthcareHospitalManagement.Domain.Interfaces.Notification
{
    public interface INotificationTemplateRepository : IGenericRepository<NotificationTemplate>
    {
        Task<NotificationTemplate?> GetByCodeAsync(string templateCode);

        Task<PagedResponse<NotificationTemplate>> GetByChannelAsync(
            NotificationChannel channel,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);

        Task<PagedResponse<NotificationTemplate>> GetByCategoryAsync(
            NotificationCategory category,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);

        Task<bool> IsTemplateCodeUniqueAsync(string templateCode);

        Task<PagedResponse<NotificationTemplate>> SearchTemplatesAsync(
            string searchTerm,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);

        // 🔹 নতুন করে যোগ করা হলো — সব templates paged আকারে আনার জন্য
        Task<PagedResponse<NotificationTemplate>> GetAllPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
