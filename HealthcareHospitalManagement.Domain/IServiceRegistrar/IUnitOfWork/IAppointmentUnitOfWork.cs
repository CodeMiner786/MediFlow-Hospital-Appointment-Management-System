using HealthcareHospitalManagement.Domain.Interfaces.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IAppointmentUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; }
        IAppointmentReminderRepository AppointmentReminders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
