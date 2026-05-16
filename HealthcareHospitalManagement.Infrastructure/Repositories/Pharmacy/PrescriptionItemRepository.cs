using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class PrescriptionItemRepository(ApplicationDbContext context)
        : GenericRepository<PrescriptionItem>(context), IPrescriptionItemRepository
    {
        private readonly DbSet<PrescriptionItem> _dbSet = context.Set<PrescriptionItem>();

        // 🔹 প্রেসক্রিপশন আইডির বিপরীতে সব মেডিসিন এবং ইনস্ট্রাকশন লোড করা
        public async Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(Guid prescriptionId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(i => i.Medicine)
                .Where(i => i.PrescriptionId == prescriptionId && !i.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 ফার্মাসিস্টের জন্য চেক করা কোন ওষুধগুলো এখনো ডেলিভারি দেওয়া বাকি
        public async Task<IEnumerable<PrescriptionItem>> GetPendingDispenseItemsAsync(Guid prescriptionId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.PrescriptionId == prescriptionId && !i.IsDispensed && !i.IsDeleted)
                .Include(i => i.Medicine)
                .ToListAsync(ct);
        }

        // 🔹 এনালিটিক্স বা হিস্টোরির জন্য নির্দিষ্ট মেডিসিনের ব্যবহার ট্র্যাকিং
        public async Task<IEnumerable<PrescriptionItem>> GetHistoryByMedicineIdAsync(Guid medicineId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.MedicineId == medicineId && !i.IsDeleted)
                .Include(i => i.Prescription)
                    .ThenInclude(p => p.Patient)
                .ToListAsync(ct);
        }

        // 🔹 ওষুধ পেশেন্টকে বুঝিয়ে দেওয়ার পর স্ট্যাটাস আপডেট
        public async Task MarkAsDispensedAsync(Guid itemId, CancellationToken ct = default)
        {
            var item = await GetByIdAsync(itemId, ct);
            if (item != null)
            {
                item.IsDispensed = true;
                item.DispensedAt = DateTime.UtcNow;
                await context.SaveChangesAsync(ct);
            }
        }
    }
}
