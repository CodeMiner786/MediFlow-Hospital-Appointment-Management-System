using HealthcareHospitalManagement.Domain.Interfaces.Analytics;
using HealthcareHospitalManagement.Domain.Interfaces.Feedback;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IAnalyticsNotificationUnitOfWork : IDisposable
    {
        // Analytics
        IDashboardMetricRepository DashboardMetrics { get; }
        IReportRepository Reports { get; }

        // Notification
        INotificationTemplateRepository NotificationTemplates { get; }

        // Feedback
        IFeedbackRepository Feedbacks { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
