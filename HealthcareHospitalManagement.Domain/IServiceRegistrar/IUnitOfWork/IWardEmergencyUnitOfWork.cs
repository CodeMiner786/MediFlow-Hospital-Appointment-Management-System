using HealthcareHospitalManagement.Domain.Interfaces.Emergency;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IWardEmergencyUnitOfWork : IDisposable
    {
        // Ward
        IWardRepository Wards { get; }
        IBedRepository Beds { get; }
        IBedBookingRepository BedBookings { get; }
        IAdmissionRepository Admissions { get; }

        // Emergency
        IEmergencyVisitRepository EmergencyVisits { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
