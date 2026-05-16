using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Patients
{
    public class MedicalRecordRepository(ApplicationDbContext context)
        : GenericRepository<MedicalRecord>(context), IMedicalRecordRepository
    {
        private readonly DbSet<MedicalRecord> _dbSet = context.Set<MedicalRecord>();

        // 🔹 পেশেন্টের কমপ্লিট হিস্টোরি (ডক্টরের নামসহ লোড করা হয়েছে)
        public async Task<IEnumerable<MedicalRecord>> GetRecordsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Include(r => r.Doctor)
                .Where(r => r.PatientId == patientId && !r.IsDeleted)
                .OrderByDescending(r => r.VisitDate)
                .ToListAsync();
        }

        // 🔹 ডক্টরের নিজস্ব ড্যাশবোর্ডের জন্য তার দেওয়া রেকর্ডগুলো
        public async Task<IEnumerable<MedicalRecord>> GetRecordsByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(r => r.Patient)
                .Where(r => r.DoctorId == doctorId && !r.IsDeleted)
                .OrderByDescending(r => r.VisitDate)
                .ToListAsync();
        }

        // 🔹 ডায়াগনোসিস দিয়ে সার্চ (যেমন: 'Diabetes' এর সব পেশেন্ট বের করা)
        public async Task<IEnumerable<MedicalRecord>> GetRecordsByDiagnosisAsync(string diagnosis)
        {
            return await _dbSet
                .Where(r => r.Diagnosis.Contains(diagnosis) && !r.IsDeleted)
                .Include(r => r.Patient)
                .ToListAsync();
        }

        // 🔹 আগামীকাল বা নির্দিষ্ট দিনে কাদের ফলো-আপ আছে তাদের লিস্ট
        public async Task<IEnumerable<MedicalRecord>> GetUpcomingFollowUpsAsync(DateTime date)
        {
            return await _dbSet
                .Include(r => r.Patient)
                .Include(r => r.Doctor)
                .Where(r => r.FollowUpDate.HasValue && r.FollowUpDate.Value.Date == date.Date && !r.IsDeleted)
                .ToListAsync();
        }

        // 🔹 পেশেন্টের বর্তমান অবস্থা বুঝতে একদম শেষ ভিজিট রিপোর্ট
        public async Task<MedicalRecord?> GetLatestRecordByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(r => r.PatientId == patientId && !r.IsDeleted)
                .OrderByDescending(r => r.VisitDate)
                .FirstOrDefaultAsync();
        }
    }
}
