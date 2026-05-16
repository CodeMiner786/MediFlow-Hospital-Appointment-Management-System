using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class MedicineStockRepository(ApplicationDbContext context)
        : GenericRepository<MedicineStock>(context), IMedicineStockRepository
    {
        private readonly DbSet<MedicineStock> _dbSet = context.Set<MedicineStock>();

        // 🔹 নির্দিষ্ট ওষুধের স্টক হিস্টোরি (সব ব্যাচসহ)
        public async Task<IEnumerable<MedicineStock>> GetStocksByMedicineIdAsync(Guid medicineId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.MedicineId == medicineId && !s.IsDeleted)
                .OrderBy(s => s.ExpiryDate)
                .ToListAsync(ct);
        }

        // 🔹 আগামী কয়েক দিনের মধ্যে এক্সপায়ার হবে এমন ওষুধের লিস্ট (e.g., আগামী ৩০ দিন)
        public async Task<IEnumerable<MedicineStock>> GetExpiringSoonStocksAsync(int daysCount, CancellationToken ct = default)
        {
            var targetDate = DateTime.UtcNow.AddDays(daysCount);
            return await _dbSet
                .Include(s => s.Medicine)
                .Where(s => s.ExpiryDate <= targetDate &&
                            s.ExpiryDate > DateTime.UtcNow &&
                            !s.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 রি-অর্ডার লেভেলের নিচে নেমে যাওয়া স্টকের লিস্ট
        public async Task<IEnumerable<MedicineStock>> GetLowStockMedicinesAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.Medicine)
                .Where(s => s.QuantityInStock <= s.ReorderLevel && !s.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 ব্যাচ নম্বর দিয়ে নিখুঁত ট্র্যাকিং
        public async Task<MedicineStock?> GetByBatchNumberAsync(string batchNumber, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.Medicine)
                .FirstOrDefaultAsync(s => s.BatchNumber == batchNumber && !s.IsDeleted,ct);
        }

        // 🔹 সাপ্লাইয়ার অনুযায়ী অডিট ট্রেইল
        public async Task<IEnumerable<MedicineStock>> GetStocksBySupplierIdAsync(Guid supplierId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.SupplierId == supplierId && !s.IsDeleted)
                .Include(s => s.Medicine)
                .ToListAsync(ct);
        }

        // 🔹 অলরেডি এক্সপায়ার হয়ে যাওয়া ওষুধ (যা সেল করা যাবে না)
        public async Task<IEnumerable<MedicineStock>> GetExpiredStocksAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.Medicine)
                .Where(s => s.ExpiryDate < DateTime.UtcNow && !s.IsDeleted)
                .ToListAsync(ct);
        }
    }
}
