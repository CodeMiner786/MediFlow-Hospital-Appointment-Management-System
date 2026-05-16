using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class PrescriptionRepository(ApplicationDbContext context)
        : GenericRepository<Prescription>(context), IPrescriptionRepository
    {
        private readonly DbSet<Prescription> _dbSet = context.Set<Prescription>();

        // 🔹 প্রেসক্রিপশন কোড দিয়ে পেশেন্ট ও ওষুধের তালিকা সহ লোড করা
        public async Task<Prescription?> GetByCodeAsync(string prescriptionCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Items)
                    .ThenInclude(i => i.Medicine)
                .FirstOrDefaultAsync(p => p.PrescriptionCode == prescriptionCode && !p.IsDeleted,ct);
        }

        // 🔹 পেশেন্টের মেডিকেল হিস্টোরি দেখার জন্য
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.PatientId == patientId && !p.IsDeleted)
                .OrderByDescending(p => p.PrescribedDate)
                .ToListAsync(ct);
        }

        // 🔹 ডক্টরের ড্যাশবোর্ডের জন্য তার দেওয়া প্রেসক্রিপশন লিস্ট
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByDoctorIdAsync(Guid doctorId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.DoctorId == doctorId && !p.IsDeleted)
                .OrderByDescending(p => p.PrescribedDate)
                .ToListAsync(ct);
        }

        // 🔹 ফার্মাসিস্টের ফিল্টার করার সুবিধার জন্য
        public async Task<IEnumerable<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.Status == status && !p.IsDeleted)
                .Include(p => p.Patient)
                .ToListAsync(ct);
        }

        // 🔹 ভ্যালিডিটি শেষ হয়ে যাওয়া প্রেসক্রিপশন ট্র্যাকিং
        public async Task<IEnumerable<Prescription>> GetExpiredPrescriptionsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.ValidUntil < DateTime.UtcNow && !p.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 ক্রনিক ডিজিজ পেশেন্টদের জন্য রিফিল ট্র্যাকিং
        public async Task<IEnumerable<Prescription>> GetRefillablePrescriptionsAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.PatientId == patientId &&
                            p.IsRefillable &&
                            p.RefillCount > 0 &&
                            !p.IsDeleted)
                .ToListAsync(ct);
        }
    }
}
