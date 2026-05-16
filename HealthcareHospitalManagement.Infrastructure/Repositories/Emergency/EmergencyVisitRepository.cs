using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Interfaces.Emergency;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Emergency
{
    public class EmergencyVisitRepository(ApplicationDbContext context)
        : GenericRepository<EmergencyVisit>(context), IEmergencyVisitRepository
    {
        private readonly DbSet<EmergencyVisit> _dbSet = context.Set<EmergencyVisit>();

        public async Task<EmergencyVisit?> GetByEmergencyCodeAsync(string code, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(v => v.EmergencyCode == code && !v.IsDeleted, ct);
        }

        public ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetActiveEmergencyVisitsStream(CancellationToken ct)
        {
            return _dbSet
                .Where(v => v.Status != EmergencyStatus.Discharged
                         && v.Status != EmergencyStatus.Admitted
                         && !v.IsDeleted)
                .OrderByDescending(v => v.TriageLevel)
                .AsNoTracking()
                .AsAsyncEnumerable()
                .WithCancellation(ct);
        }

        public ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetVisitsByTriageLevelStream(EmergencyLevel level, CancellationToken ct)
        {
            return _dbSet
                .Where(v => v.TriageLevel == level && !v.IsDeleted)
                .OrderByDescending(v => v.ArrivalTime)
                .AsNoTracking()
                .AsAsyncEnumerable()
                .WithCancellation(ct);
        }

        public ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetHistoryByPatientIdStream(Guid patientId, CancellationToken ct)
        {
            return _dbSet
                .Where(v => v.PatientId == patientId && !v.IsDeleted)
                .OrderByDescending(v => v.ArrivalTime)
                .AsNoTracking()
                .AsAsyncEnumerable()
                .WithCancellation(ct);
        }

        public ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetVisitsByDateRangeStream(DateTime start, DateTime end, CancellationToken ct)
        {
            return _dbSet
                .Where(v => v.ArrivalTime >= start && v.ArrivalTime <= end && !v.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable()
                .WithCancellation(ct);
        }

        public ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetVisitsByStatusStream(EmergencyStatus status, CancellationToken ct)
        {
            return _dbSet
                .Where(v => v.Status == status && !v.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable()
                .WithCancellation(ct);
        }

        public async Task<EmergencyVisit?> GetEmergencyVisitWithDetailsAsync(Guid id, CancellationToken ct)
        {
            return await _dbSet
                .Include(v => v.Patient)
                .Include(v => v.AttendingDoctor)
                .Include(v => v.AmbulanceBooking)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted, ct);
        }
    }
}
