using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IAmbulanceUnitOfWork : IDisposable
    {
        IAmbulanceBookingRepository AmbulanceBookings { get; }
        IAmbulanceProviderProfileRepository AmbulanceProviderProfiles { get; }
        IAmbulanceProviderWalletRepository AmbulanceProviderWallets { get; }
        IAmbulanceServiceListingRepository AmbulanceServiceListings { get; }

        // ✅ নতুন property যোগ করো
        IAmbulanceVehicleRepository AmbulanceVehicles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
