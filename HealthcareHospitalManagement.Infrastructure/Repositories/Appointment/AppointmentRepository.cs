using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Enums.Appointment;
using HealthcareHospitalManagement.Domain.Interfaces.Appointment;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Appointment
{
    public class AppointmentRepository(ApplicationDbContext context)
        : GenericRepository<AppointmentEntity>(context), IAppointmentRepository
    {
        private readonly DbSet<AppointmentEntity> _dbSet = context.Set<AppointmentEntity>();

        public async Task<AppointmentEntity?> GetByAppointmentCodeAsync(string appointmentCode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.AppointmentCode == appointmentCode && !a.IsDeleted);
        }

        public IAsyncEnumerable<AppointmentEntity> GetDoctorAppointmentsByDateStream(Guid doctorId, DateTime date)
        {
            return _dbSet
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date && !a.IsDeleted)
                .OrderBy(a => a.StartTime)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<AppointmentEntity> GetPatientAppointmentHistoryStream(Guid patientId)
        {
            return _dbSet
                .Where(a => a.PatientId == patientId && !a.IsDeleted)
                .OrderByDescending(a => a.AppointmentDate)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<AppointmentEntity> GetAppointmentsByStatusStream(AppointmentStatus status)
        {
            return _dbSet
                .Where(a => a.Status == status && !a.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ✅ নতুন method: Doctor availability check
        public async Task<bool> IsDoctorBookedAsync(Guid doctorId, DateTime appointmentDate, TimeOnly startTime, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AnyAsync(a => a.DoctorId == doctorId
                            && a.AppointmentDate.Date == appointmentDate.Date
                            && a.StartTime == startTime
                            && !a.IsDeleted,
                          cancellationToken);
        }
    }
}
