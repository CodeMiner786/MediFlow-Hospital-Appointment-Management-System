using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    public class DoctorNotificationRepository(ApplicationDbContext context)
        : GenericRepository<DoctorNotification>(context), IDoctorNotificationRepository
    {
        private readonly DbSet<DoctorNotification> _dbSet = context.Set<DoctorNotification>();

        public IAsyncEnumerable<DoctorNotification> GetNotificationsByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(n => n.DoctorId == doctorId && !n.IsDeleted)
                .OrderByDescending(n => n.DeliveredAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<DoctorNotification> GetUnreadNotificationsByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(n => n.DoctorId == doctorId && !n.IsRead && !n.IsDeleted)
                .OrderByDescending(n => n.DeliveredAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<DoctorNotification> GetUrgentNotificationsStream(Guid doctorId)
        {
            return _dbSet
                .Where(n => n.DoctorId == doctorId && n.IsUrgent && !n.IsDeleted)
                .OrderByDescending(n => n.DeliveredAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task MarkAllAsReadAsync(Guid doctorId, CancellationToken ct = default)
        {
            await _dbSet
                .Where(n => n.DoctorId == doctorId && !n.IsRead && !n.IsDeleted)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(n => n.IsRead, true)
                    .SetProperty(n => n.ReadAt, DateTime.UtcNow), ct);
        }

        public async Task MarkAsReadAsync(Guid notificationId, CancellationToken ct = default)
        {
            var notification = await GetByIdAsync(notificationId, ct);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(notification, ct);
                await context.SaveChangesAsync(ct);
            }
        }

        public IAsyncEnumerable<DoctorNotification> GetByReferenceIdAsync(Guid referenceId)
        {
            return _dbSet
                .Where(n => n.ReferenceId == referenceId && !n.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
