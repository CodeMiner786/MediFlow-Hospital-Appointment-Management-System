using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Patients
{
    public class PatientVitalRepository(ApplicationDbContext context)
        : GenericRepository<PatientVital>(context), IPatientVitalRepository
    {
        private readonly DbSet<PatientVital> _dbSet = context.Set<PatientVital>();

        // 🔹 পেশেন্টের সম্পূর্ণ ভাইটাল হিস্টোরি (টাইমলাইন অনুযায়ী)
        public async Task<IEnumerable<PatientVital>> GetVitalsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(v => v.PatientId == patientId && !v.IsDeleted)
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        // 🔹 ডক্টরের জন্য পেশেন্টের সবথেকে নতুন ভাইটাল ইনফো
        public async Task<PatientVital?> GetLatestVitalsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(v => v.PatientId == patientId && !v.IsDeleted)
                .OrderByDescending(v => v.RecordedAt)
                .FirstOrDefaultAsync();
        }

        // 🔹 নির্দিষ্ট সময়ের মধ্যে ভাইটাল ডেটা (গ্রাফ বা রিপোর্টের জন্য)
        public async Task<IEnumerable<PatientVital>> GetVitalsByDateRangeAsync(Guid patientId, DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(v => v.PatientId == patientId &&
                            v.RecordedAt >= start &&
                            v.RecordedAt <= end &&
                            !v.IsDeleted)
                .OrderBy(v => v.RecordedAt)
                .ToListAsync();
        }

        // 🔹 ক্রিটিক্যাল ভাইটাল ফিল্টারিং (যেমন: হাই ফিভার বা লো অক্সিজেন লেভেল)
        public async Task<IEnumerable<PatientVital>> GetCriticalVitalsAsync(Guid patientId)
        {
            return await _dbSet
                .Where(v => v.PatientId == patientId && !v.IsDeleted &&
                           (v.TemperatureCelsius > 39 ||
                            v.OxygenSaturation < 94 ||
                            v.PulseRate > 110 ||
                            v.BloodPressureSystolic > 150))
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        // 🔹 অডিট পারপাসে রেকর্ডারের নাম দিয়ে সার্চ
        public async Task<IEnumerable<PatientVital>> GetVitalsByRecorderNameAsync(string recorderName)
        {
            return await _dbSet
                .Where(v => v.RecordedByName.Contains(recorderName) && !v.IsDeleted)
                .ToListAsync();
        }
    }
}
