using HealthcareHospitalManagement.Domain.Interfaces.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IDashboardUnitOfWork : IDisposable
    {
        IAdminDashboardMetricRepository AdminDashboardMetrics { get; }
        IAmbulanceDashboardMetricRepository AmbulanceDashboardMetrics { get; }
        IDoctorDashboardMetricRepository DoctorDashboardMetrics { get; }
        ILabDashboardMetricRepository LabDashboardMetrics { get; }
        IPharmacyDashboardMetricRepository PharmacyDashboardMetrics { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
