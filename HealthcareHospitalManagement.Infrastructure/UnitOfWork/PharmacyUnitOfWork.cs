using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy;


namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class PharmacyUnitOfWork(ApplicationDbContext context) : IPharmacyUnitOfWork
    {
        public IPrescriptionRepository Prescriptions { get; } = new PrescriptionRepository(context);
        public IPrescriptionItemRepository PrescriptionItems { get; } = new PrescriptionItemRepository(context);
        public IMedicineRepository Medicines { get; } = new MedicineRepository(context);
        public IMedicineOrderRepository MedicineOrders { get; } = new MedicineOrderRepository(context);
        public IMedicineOrderItemRepository MedicineOrderItems { get; } = new MedicineOrderItemRepository(context);
        public IMedicineStockRepository MedicineStocks { get; } = new MedicineStockRepository(context);
        public IPharmacyProfileRepository PharmacyProfiles { get; } = new PharmacyProfileRepository(context);
        public IPharmacyServiceListingRepository PharmacyServiceListings { get; } = new PharmacyServiceListingRepository(context);
        public ISupplierRepository Suppliers { get; } = new SupplierRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
