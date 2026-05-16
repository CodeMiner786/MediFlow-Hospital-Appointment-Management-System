using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class MedicineRepository(ApplicationDbContext context)
        : GenericRepository<Medicine>(context), IMedicineRepository
    {
        private readonly DbSet<Medicine> _dbSet = context.Set<Medicine>();

        // 🔹 বারকোড বা ইউনিক কোড স্ক্যান করে ওষুধ বের করা
        public async Task<Medicine?> GetByCodeAsync(string medicineCode,CancellationToken ct= default)
        {
            return await _dbSet
                .Include(m => m.Stocks)
                .FirstOrDefaultAsync(m => m.MedicineCode == medicineCode && !m.IsDeleted, ct);
        }

        // 🔹 ডাক্তারদের জন্য জেনেরিক অনুযায়ী বিকল্প ওষুধ খোঁজা
        public async Task<IEnumerable<Medicine>> GetByGenericNameAsync(string genericName, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(m => m.GenericName.Contains(genericName) && !m.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 নির্দিষ্ট ক্যাটাগরির ওষুধের লিস্ট
        public async Task<IEnumerable<Medicine>> GetByCategoryAsync(MedicineCategory category, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(m => m.Category == category && !m.IsDeleted)
                .ToListAsync(ct);
        }


        // 🔹 গ্লোবাল সার্চ (নাম, ব্র্যান্ড বা জেনেরিক দিয়ে)
        public async Task<IEnumerable<Medicine>> SearchMedicinesAsync(string searchTerm, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(m => (m.MedicineName.Contains(searchTerm) ||
                             m.BrandName.Contains(searchTerm) ||
                             m.GenericName.Contains(searchTerm)) && !m.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 সেফটি চেক: প্রেসক্রিপশন ছাড়া বিক্রি করা যাবে না এমন ওষুধের লিস্ট
        public async Task<IEnumerable<Medicine>> GetPrescriptionRequiredMedicinesAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(m => m.RequiresPrescription && !m.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 নির্দিষ্ট ফার্মেসির স্টকে কি কি ওষুধ আছে
        public async Task<IEnumerable<Medicine>> GetMedicinesByPharmacyIdAsync(Guid pharmacyId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(m => m.PharmacyProfileId == pharmacyId && !m.IsDeleted)
                .Include(m => m.Stocks)
                .ToListAsync(ct);
        }
    }
}
