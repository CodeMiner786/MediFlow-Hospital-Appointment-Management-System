using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class MedicineOrderRepository(ApplicationDbContext context)
        : GenericRepository<MedicineOrder>(context), IMedicineOrderRepository
    {
        private readonly DbSet<MedicineOrder> _dbSet = context.Set<MedicineOrder>();

        // 🔹 অর্ডার কোড দিয়ে ডিটেইলস এবং আইটেমসহ লোড করা
        public async Task<MedicineOrder?> GetByOrderCodeAsync(string orderCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(o => o.Patient)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Medicine)
                .FirstOrDefaultAsync(o => o.OrderCode == orderCode && !o.IsDeleted, ct);
        }

        // 🔹 পেশেন্ট তার অ্যাপ থেকে নিজের সব অর্ডার দেখতে পারবে
        public async Task<IEnumerable<MedicineOrder>> GetOrdersByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(o => o.PatientId == patientId && !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(ct);
        }

        // 🔹 প্রেসক্রিপশন থেকে ওষুধ কেনা হয়েছে কি না চেক করা
        public async Task<MedicineOrder?> GetOrderByPrescriptionIdAsync(Guid prescriptionId, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(o => o.PrescriptionId == prescriptionId && !o.IsDeleted, ct);
        }

        // 🔹 ফার্মাসিস্টের জন্য স্ট্যাটাস অনুযায়ী ডেলিভারি ম্যানেজমেন্ট
        public async Task<IEnumerable<MedicineOrder>> GetOrdersByStatusAsync(MedicineOrderStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(o => o.Status == status && !o.IsDeleted)
                .Include(o => o.Patient)
                .ToListAsync(ct);
        }

        // 🔹 পেমেন্ট কালেকশন ট্র্যাকিং
        public async Task<IEnumerable<MedicineOrder>> GetOrdersByPaymentStatusAsync(bool isPaid, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(o => o.IsPaid == isPaid && !o.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 প্রতিদিনের সেলস অডিট
        public async Task<IEnumerable<MedicineOrder>> GetTodaysOrdersAsync(CancellationToken ct = default)
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet
                .Where(o => o.OrderDate.Date == today && !o.IsDeleted)
                .Include(o => o.Items)
                .ToListAsync(ct);
        }
    }
}
