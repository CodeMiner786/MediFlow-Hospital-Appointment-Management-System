using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.Patient;
using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Patients
{
    public class PatientRepository(ApplicationDbContext context)
        : GenericRepository<Patient>(context), IPatientRepository
    {
        private readonly DbSet<Patient> _dbSet = context.Set<Patient>();

        public async Task<Patient?> GetByPatientCodeAsync(string patientCode, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.PatientCode == patientCode && !p.IsDeleted, ct);
        }

        public async Task<Patient?> GetByUserIdAsync(Guid userId, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.ApplicationUserId == userId && !p.IsDeleted, ct);
        }

        public async Task<Patient?> GetByNationalIdAsync(string nid, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.NationalId == nid && !p.IsDeleted, ct);
        }

        public async Task<Patient?> GetFullProfileByIdAsync(Guid patientId, CancellationToken ct)
        {
            return await _dbSet
                .Include(p => p.Vitals)
                .Include(p => p.MedicalRecords)
                .Include(p => p.HealthLogs)
                .Include(p => p.Admissions)
                .FirstOrDefaultAsync(p => p.Id == patientId && !p.IsDeleted, ct);
        }

        public async Task<IEnumerable<Patient>> GetPatientsByBloodGroupAsync(BloodGroup bloodGroup, CancellationToken ct)
        {
            return await _dbSet
                .Where(p => p.BloodGroup == bloodGroup && !p.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm, CancellationToken ct)
        {
            return await _dbSet
                .Where(p => (p.FirstName.Contains(searchTerm) ||
                             p.LastName.Contains(searchTerm) ||
                             p.PhoneNumber.Contains(searchTerm)) && !p.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Patient>> GetCurrentlyAdmittedPatientsAsync(CancellationToken ct)
        {
            return await _dbSet
                .Include(p => p.Admissions)
                .Where(p => p.Admissions.Any(a => a.DischargeDate == null) && !p.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<Patient?> GetByApplicationUserIdAsync(Guid applicationUserId, CancellationToken ct)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.ApplicationUserId == applicationUserId && !p.IsDeleted, ct);
        }
    }
}