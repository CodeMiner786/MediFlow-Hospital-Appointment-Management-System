using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IPharmacyUnitOfWork : IDisposable
    {
        IPrescriptionRepository Prescriptions { get; }
        IPrescriptionItemRepository PrescriptionItems { get; }
        IMedicineRepository Medicines { get; }
        IMedicineOrderRepository MedicineOrders { get; }
        IMedicineOrderItemRepository MedicineOrderItems { get; }
        IMedicineStockRepository MedicineStocks { get; }
        IPharmacyProfileRepository PharmacyProfiles { get; }
        IPharmacyServiceListingRepository PharmacyServiceListings { get; }
        ISupplierRepository Suppliers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
