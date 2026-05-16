using HealthcareHospitalManagement.Domain.Interfaces.Telemedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface ITelemedicineUnitOfWork : IDisposable
    {
        ITelemedicineSessionRepository TelemedicineSessions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
