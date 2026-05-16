using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        // ১. সাপ্লাইয়ার কোড দিয়ে খুঁজে বের করা
        Task<Supplier?> GetByCodeAsync(string supplierCode, CancellationToken ct = default);

        // ২. নির্দিষ্ট স্ট্যাটাস অনুযায়ী সাপ্লাইয়ার লিস্ট (Active/Inactive)
        Task<IEnumerable<Supplier>> GetSuppliersByStatusAsync(SupplierStatus status, CancellationToken ct = default);

        // ৩. কোম্পানির নাম বা কন্টাক্ট পারসনের নাম দিয়ে সার্চ করা
        Task<IEnumerable<Supplier>> SearchSuppliersAsync(string searchTerm, CancellationToken ct = default);

        // ৪. একটি নির্দিষ্ট সাপ্লাইয়ার থেকে কতগুলো মেডিসিন স্টকে আছে তা দেখা
        Task<int> GetTotalStockCountBySupplierIdAsync(Guid supplierId, CancellationToken ct = default);

        // ৫. সাপ্লাইয়ারের ইমেইল দিয়ে ডাটা খুঁজে বের করা (ডুপ্লিকেট চেক করার জন্য)
        Task<Supplier?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
