using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Patients
{
    public class PatientReferralRepository(ApplicationDbContext context)
        : GenericRepository<PatientReferral>(context), IPatientReferralRepository
    {
        private readonly DbSet<PatientReferral> _dbSet = context.Set<PatientReferral>();

        // 🔹 ইউনিক রেফারেল কোড দিয়ে ডিটেইলস লোড
        public async Task<PatientReferral?> GetByReferralCodeAsync(string referralCode)
        {
            return await _dbSet
                .Include(r => r.Patient)
                .Include(r => r.ReferringDoctor)
                .Include(r => r.ReferredToDoctor)
                .FirstOrDefaultAsync(r => r.ReferralCode == referralCode && !r.IsDeleted);
        }

        // 🔹 পেশেন্টের পোর্টালের জন্য তার রেফারেল লিস্ট
        public async Task<IEnumerable<PatientReferral>> GetReferralsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(r => r.PatientId == patientId && !r.IsDeleted)
                .Include(r => r.ReferringDoctor)
                .OrderByDescending(r => r.ReferralDate)
                .ToListAsync();
        }

        // 🔹 ডাক্তারের ড্যাশবোর্ডে তিনি কাদের রেফার করেছেন তার লিস্ট
        public async Task<IEnumerable<PatientReferral>> GetOutgoingReferralsByDoctorIdAsync(Guid referringDoctorId)
        {
            return await _dbSet
                .Where(r => r.ReferringDoctorId == referringDoctorId && !r.IsDeleted)
                .Include(r => r.Patient)
                .ToListAsync();
        }

        // 🔹 স্পেশালিস্ট ডাক্তারের জন্য তার কাছে আসা রেফারেলগুলো দেখা
        public async Task<IEnumerable<PatientReferral>> GetIncomingReferralsByDoctorIdAsync(Guid referredToDoctorId)
        {
            return await _dbSet
                .Where(r => r.ReferredToDoctorId == referredToDoctorId && !r.IsDeleted)
                .Include(r => r.Patient)
                .Include(r => r.ReferringDoctor)
                .ToListAsync();
        }

        // 🔹 'High' বা 'Emergency' প্রায়োরিটির রেফারেলগুলো দ্রুত খুঁজে বের করা
        public async Task<IEnumerable<PatientReferral>> GetPendingUrgentReferralsAsync()
        {
            return await _dbSet
                .Where(r => !r.IsAccepted &&
                           (r.UrgencyLevel == "High" || r.UrgencyLevel == "Emergency") &&
                           !r.IsDeleted)
                .Include(r => r.Patient)
                .OrderByDescending(r => r.ReferralDate)
                .ToListAsync();
        }

        // 🔹 ডিপার্টমেন্ট ভিত্তিক রেফারেল ফিল্টারিং
        public async Task<IEnumerable<PatientReferral>> GetReferralsByDepartmentAsync(string departmentName)
        {
            return await _dbSet
                .Where(r => r.ReferredToDepartment == departmentName && !r.IsDeleted)
                .Include(r => r.Patient)
                .ToListAsync();
        }
    }
}
