using HealthcareHospitalManagement.Domain.Interfaces.Analytics;
using HealthcareHospitalManagement.Domain.Interfaces.Feedback;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Analytics;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feedback;
using HealthcareHospitalManagement.Infrastructure.Repositories.Notification;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class AnalyticsNotificationUnitOfWork(ApplicationDbContext context) : IAnalyticsNotificationUnitOfWork
    {
        // Analytics
        public IDashboardMetricRepository DashboardMetrics { get; } = new DashboardMetricRepository(context);
        public IReportRepository Reports { get; } = new ReportRepository(context);

        // Notification
        public INotificationTemplateRepository NotificationTemplates { get; } = new NotificationTemplateRepository(context);

        // Feedback
        public IFeedbackRepository Feedbacks { get; } = new FeedbackRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
