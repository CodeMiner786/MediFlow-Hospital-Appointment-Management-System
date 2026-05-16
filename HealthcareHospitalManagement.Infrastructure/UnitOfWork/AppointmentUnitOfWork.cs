using HealthcareHospitalManagement.Domain.Interfaces.Appointment;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class AppointmentUnitOfWork(ApplicationDbContext context) : IAppointmentUnitOfWork
    {
        public IAppointmentRepository Appointments { get; } = new AppointmentRepository(context);
        public IAppointmentReminderRepository AppointmentReminders { get; } = new AppointmentReminderRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
