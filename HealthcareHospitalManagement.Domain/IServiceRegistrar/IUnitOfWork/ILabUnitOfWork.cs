using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface ILabUnitOfWork : IDisposable
    {
        ILabOrderRepository LabOrders { get; }
        ILabOrderItemRepository LabOrderItems { get; }
        ILabProfileRepository LabProfiles { get; }
        ILabServiceListingRepository LabServiceListings { get; }
        ILabTestRepository LabTests { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
