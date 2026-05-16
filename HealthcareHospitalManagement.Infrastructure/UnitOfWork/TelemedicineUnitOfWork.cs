using HealthcareHospitalManagement.Domain.Interfaces.Telemedicine;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Telemedicine;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class TelemedicineUnitOfWork(ApplicationDbContext context) : ITelemedicineUnitOfWork
    {
        public ITelemedicineSessionRepository TelemedicineSessions { get; } = new TelemedicineSessionRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
