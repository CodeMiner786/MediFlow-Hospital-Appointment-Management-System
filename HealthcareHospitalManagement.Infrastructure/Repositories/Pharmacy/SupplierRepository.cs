using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class SupplierRepository(ApplicationDbContext context)
        : GenericRepository<Supplier>(context), ISupplierRepository
    {
        private readonly DbSet<Supplier> _dbSet = context.Set<Supplier>();

        // 🔹 ইউনিক সাপ্লাইয়ার কোড দিয়ে সার্চ
        public async Task<Supplier?> GetByCodeAsync(string supplierCode, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.SupplierCode == supplierCode && !s.IsDeleted, ct);
        }

        // 🔹 স্ট্যাটাস অনুযায়ী ফিল্টার (যেমন: ব্ল্যাকলিস্টেড সাপ্লাইয়ারদের বাদ দেওয়া)
        public async Task<IEnumerable<Supplier>> GetSuppliersByStatusAsync(SupplierStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.Status == status && !s.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 গ্লোবাল সার্চ (কোম্পানির নাম বা কন্টাক্ট পারসন দিয়ে)
        public async Task<IEnumerable<Supplier>> SearchSuppliersAsync(string searchTerm, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => (s.SupplierName.Contains(searchTerm) ||
                             s.ContactPerson.Contains(searchTerm)) && !s.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 সাপ্লাইয়ারের পারফরম্যান্স বা ডিপেন্ডেন্সি চেক করার জন্য
        public async Task<int> GetTotalStockCountBySupplierIdAsync(Guid supplierId, CancellationToken ct = default)
        {
            return await context.Set<MedicineStock>()
                .CountAsync(st => st.SupplierId == supplierId && !st.IsDeleted, ct);
        }

        // 🔹 রেজিস্ট্রেশনের সময় ডুপ্লিকেট ইমেইল চেক
        public async Task<Supplier?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Email == email && !s.IsDeleted, ct);
        }
    }
}
