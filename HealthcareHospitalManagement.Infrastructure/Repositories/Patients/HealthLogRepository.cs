using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Patients
{
    public class HealthLogRepository(ApplicationDbContext context)
        : GenericRepository<HealthLog>(context), IHealthLogRepository
    {
        private readonly DbSet<HealthLog> _dbSet = context.Set<HealthLog>();

        // 🔹 পেশেন্টের সব হিস্টোরিক্যাল ডেটা লোড করা
        public async Task<IEnumerable<HealthLog>> GetLogsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(l => l.PatientId == patientId && !l.IsDeleted)
                .OrderByDescending(l => l.LoggedAt)
                .ToListAsync();
        }

        // 🔹 ড্যাশবোর্ডে পেশেন্টের বর্তমান অবস্থা দেখানোর জন্য লেটেস্ট রেকর্ড
        public async Task<HealthLog?> GetLatestLogByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(l => l.PatientId == patientId && !l.IsDeleted)
                .OrderByDescending(l => l.LoggedAt)
                .FirstOrDefaultAsync();
        }

        // 🔹 গ্রাফ বা চার্ট তৈরির জন্য নির্দিষ্ট সময়ের ডেটা
        public async Task<IEnumerable<HealthLog>> GetLogsByDateRangeAsync(Guid patientId, DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(l => l.PatientId == patientId &&
                            l.LoggedAt >= start &&
                            l.LoggedAt <= end &&
                            !l.IsDeleted)
                .OrderBy(l => l.LoggedAt)
                .ToListAsync();
        }

        // 🔹 ক্রিটিক্যাল কন্ডিশন চেক করার জন্য (সিম্পল লজিক উদাহরণ)
        public async Task<IEnumerable<HealthLog>> GetAbnormalLogsAsync(Guid patientId)
        {
            return await _dbSet
                .Where(l => l.PatientId == patientId && !l.IsDeleted &&
                           (l.BloodPressureSystolic > 140 ||
                            l.OxygenSaturation < 95 ||
                            l.BloodGlucose > 10))
                .OrderByDescending(l => l.LoggedAt)
                .ToListAsync();
        }

        // 🔹 ডিভাইস ভিত্তিক ডেটা ট্র্যাকিং
        public async Task<IEnumerable<HealthLog>> GetLogsBySourceAsync(Guid patientId, string source)
        {
            return await _dbSet
                .Where(l => l.PatientId == patientId &&
                            l.Source == source &&
                            !l.IsDeleted)
                .ToListAsync();
        }
    }
}
