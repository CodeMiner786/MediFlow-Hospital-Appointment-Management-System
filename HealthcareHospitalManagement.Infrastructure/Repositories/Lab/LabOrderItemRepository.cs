using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Lab;
using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Lab
{
    public class LabOrderItemRepository(ApplicationDbContext context)
        : GenericRepository<LabOrderItem>(context), ILabOrderItemRepository
    {
        private readonly DbSet<LabOrderItem> _dbSet = context.Set<LabOrderItem>();

        // 🔹 অর্ডারের আইডি দিয়ে সব টেস্ট ডিটেইলস লোড করা
        public async Task<IEnumerable<LabOrderItem>> GetItemsByOrderIdAsync(Guid orderId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.LabOrderId == orderId && !i.IsDeleted)
                .Include(i => i.LabTest)
                .ToListAsync(ct);
        }

        // 🔹 বারকোড স্ক্যান করলে সরাসরি ওই টেস্টটি চলে আসবে
        public async Task<LabOrderItem?> GetByBarcodeAsync(string barcode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(i => i.LabOrder)
                .ThenInclude(o => o.Patient)
                .FirstOrDefaultAsync(i => i.SampleBarcode == barcode && !i.IsDeleted, ct);
        }

        // 🔹 পেন্ডিং কালেকশন বা পেন্ডিং রেজাল্ট দেখা
        public async Task<IEnumerable<LabOrderItem>> GetItemsByStatusAsync(LabTestStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.Status == status && !i.IsDeleted)
                .Include(i => i.LabTest)
                .Include(i => i.LabOrder.Patient)
                .ToListAsync(ct);
        }

        // 🔹 ক্রিটিক্যাল রিপোর্ট মনিটরিং
        public async Task<IEnumerable<LabOrderItem>> GetAbnormalResultsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.IsAbnormal && i.Status == LabTestStatus.Completed && !i.IsDeleted)
                .Include(i => i.LabOrder.Patient)
                .Include(i => i.LabTest)
                .OrderByDescending(i => i.ResultEnteredAt)
                .ToListAsync(ct);
        }

        // 🔹 স্যাম্পল কালেক্ট করার সময় ডেটা আপডেট
        public async Task UpdateSampleStatusAsync(Guid itemId, string collectedBy, string barcode, CancellationToken ct = default)
        {
            var item = await GetByIdAsync(itemId, ct);
            if (item != null)
            {
                item.Status = LabTestStatus.SampleCollected;
                item.SampleCollectedAt = DateTime.UtcNow;
                item.SampleCollectedBy = collectedBy;
                item.SampleBarcode = barcode;
                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 টেস্ট রেজাল্ট ইনপুট দেওয়ার মেথড
        public async Task UpdateTestResultAsync(Guid itemId, string resultValue, bool isAbnormal, string enteredBy, CancellationToken ct = default)
        {
            var item = await GetByIdAsync(itemId, ct);
            if (item != null)
            {
                item.ResultValue = resultValue;
                item.IsAbnormal = isAbnormal;
                item.ResultEnteredBy = enteredBy;
                item.ResultEnteredAt = DateTime.UtcNow;
                item.Status = LabTestStatus.Completed;
                await context.SaveChangesAsync(ct);
            }
        }

        // ✅ নতুন — GetByLabOrderIdAsync যোগ করা হয়েছে
        public async Task<IEnumerable<LabOrderItem>> GetByLabOrderIdAsync(Guid labOrderId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(i => i.LabTest)
                .Where(i => i.LabOrderId == labOrderId && !i.IsDeleted)
                .OrderBy(i => i.CreatedAt)
                .ToListAsync(ct);
        }
    }
}