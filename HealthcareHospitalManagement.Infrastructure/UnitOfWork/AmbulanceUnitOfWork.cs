using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class AmbulanceUnitOfWork(ApplicationDbContext context) : IAmbulanceUnitOfWork, IDisposable
    {
        public IAmbulanceBookingRepository AmbulanceBookings { get; } = new AmbulanceBookingRepository(context);
        public IAmbulanceProviderProfileRepository AmbulanceProviderProfiles { get; } = new AmbulanceProviderProfileRepository(context);
        public IAmbulanceProviderWalletRepository AmbulanceProviderWallets { get; } = new AmbulanceProviderWalletRepository(context);
        public IAmbulanceServiceListingRepository AmbulanceServiceListings { get; } = new AmbulanceServiceListingRepository(context);

        // ✅ নতুন property implement করো
        public IAmbulanceVehicleRepository AmbulanceVehicles { get; } = new AmbulanceVehicleRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
