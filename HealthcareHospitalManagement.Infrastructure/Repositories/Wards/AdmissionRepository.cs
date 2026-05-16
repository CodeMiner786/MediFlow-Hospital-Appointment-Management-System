using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Wards
{
    public class AdmissionRepository(ApplicationDbContext context)
        : GenericRepository<Admission>(context), IAdmissionRepository
    {
        private readonly DbSet<Admission> _dbSet = context.Set<Admission>();

        // 🔹 অ্যাডমিশন কোড দিয়ে পেশেন্ট, ডাক্তার এবং বেড ডিটেইলস সহ লোড করা
        public async Task<Admission?> GetByAdmissionCodeAsync(string admissionCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(a => a.Patient)
                .Include(a => a.AdmittingDoctor)
                .Include(a => a.Bed)
                    .ThenInclude(b => b.Ward)
                .FirstOrDefaultAsync(a => a.AdmissionCode == admissionCode && !a.IsDeleted, ct);
        }

        // 🔹 বর্তমানে যারা ভর্তি আছেন (IPD Active List)
        public async Task<IEnumerable<Admission>> GetCurrentlyAdmittedPatientsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(a => a.Status == AdmissionStatus.Admitted && !a.IsDeleted)
                .Include(a => a.Patient)
                .Include(a => a.Bed)
                .ToListAsync(ct);
        }

        // 🔹 পেশেন্টের পুরনো ভর্তি রেকর্ড
        public async Task<IEnumerable<Admission>> GetPatientAdmissionHistoryAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(a => a.PatientId == patientId && !a.IsDeleted)
                .OrderByDescending(a => a.AdmissionDate)
                .ToListAsync(ct);
        }

        // 🔹 ডাক্তারের রাউন্ড লিস্ট
        public async Task<IEnumerable<Admission>> GetAdmissionsByDoctorIdAsync(Guid doctorId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(a => a.AdmittingDoctorId == doctorId &&
                            a.Status == AdmissionStatus.Admitted &&
                            !a.IsDeleted)
                .Include(a => a.Patient)
                .Include(a => a.Bed)
                .ToListAsync(ct);
        }

        // 🔹 বেড ম্যানেজমেন্টের জন্য বর্তমান অকুপেন্সি চেক
        public async Task<Admission?> GetCurrentAdmissionByBedIdAsync(Guid bedId, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.BedId == bedId &&
                                         a.Status == AdmissionStatus.Admitted &&
                                         !a.IsDeleted, ct);
        }

        // 🔹 স্ট্যাটাস অনুযায়ী ফিল্টার
        public async Task<IEnumerable<Admission>> GetByStatusAsync(AdmissionStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(a => a.Status == status && !a.IsDeleted)
                .Include(a => a.Patient)
                .ToListAsync(ct);
        }

        // 🔹 ডিসচার্জ লজিক আপডেট
        public async Task DischargePatientAsync(Guid admissionId, string notes, string summary, CancellationToken ct = default)
        {
            var admission = await GetByIdAsync(admissionId, ct);
            if (admission != null)
            {
                admission.Status = AdmissionStatus.Discharged;
                admission.DischargeDate = DateTime.UtcNow;
                admission.DischargeNotes = notes;
                admission.DischargeSummary = summary;

                await context.SaveChangesAsync(ct);
            }
        }
    }
}
