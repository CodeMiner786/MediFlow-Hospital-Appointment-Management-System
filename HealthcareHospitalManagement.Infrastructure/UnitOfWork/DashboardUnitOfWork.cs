using HealthcareHospitalManagement.Domain.Interfaces.Dashboard;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Dashboard;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class DashboardUnitOfWork(ApplicationDbContext context) : IDashboardUnitOfWork
    {
        public IAdminDashboardMetricRepository AdminDashboardMetrics { get; } = new AdminDashboardMetricRepository(context);
        public IAmbulanceDashboardMetricRepository AmbulanceDashboardMetrics { get; } = new AmbulanceDashboardMetricRepository(context);
        public IDoctorDashboardMetricRepository DoctorDashboardMetrics { get; } = new DoctorDashboardMetricRepository(context);
        public ILabDashboardMetricRepository LabDashboardMetrics { get; } = new LabDashboardMetricRepository(context);
        public IPharmacyDashboardMetricRepository PharmacyDashboardMetrics { get; } = new PharmacyDashboardMetricRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
