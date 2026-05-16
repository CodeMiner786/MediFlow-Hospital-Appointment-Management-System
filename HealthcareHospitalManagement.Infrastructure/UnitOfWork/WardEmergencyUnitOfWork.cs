using HealthcareHospitalManagement.Domain.Interfaces.Emergency;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Emergency;
using HealthcareHospitalManagement.Infrastructure.Repositories.Wards;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class WardEmergencyUnitOfWork(ApplicationDbContext context) : IWardEmergencyUnitOfWork
    {
        public IWardRepository Wards { get; } = new WardRepository(context);
        public IBedRepository Beds { get; } = new BedRepository(context);
        public IBedBookingRepository BedBookings { get; } = new BedBookingRepository(context);
        public IAdmissionRepository Admissions { get; } = new AdmissionRepository(context);
        public IEmergencyVisitRepository EmergencyVisits { get; } = new EmergencyVisitRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
