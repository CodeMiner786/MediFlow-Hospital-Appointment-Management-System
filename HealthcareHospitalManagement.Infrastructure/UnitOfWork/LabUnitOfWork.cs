using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Lab;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class LabUnitOfWork(ApplicationDbContext context) : ILabUnitOfWork
    {
        public ILabOrderRepository LabOrders { get; } = new LabOrderRepository(context);
        public ILabOrderItemRepository LabOrderItems { get; } = new LabOrderItemRepository(context);
        public ILabProfileRepository LabProfiles { get; } = new LabProfileRepository(context);
        public ILabServiceListingRepository LabServiceListings { get; } = new LabServiceListingRepository(context);
        public ILabTestRepository LabTests { get; } = new LabTestRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
