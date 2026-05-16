using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface IMedicineOrderItemRepository : IGenericRepository<MedicineOrderItem>
    {
        // ১. একটি নির্দিষ্ট অর্ডারের সব আইটেম লিস্ট দেখা
        Task<IEnumerable<MedicineOrderItem>> GetItemsByOrderIdAsync(Guid orderId, CancellationToken ct = default);

        // ২. নির্দিষ্ট কোনো ওষুধ কতবার বা কতটুকু বিক্রি হয়েছে তা দেখা (Analytics)
        Task<IEnumerable<MedicineOrderItem>> GetItemsByMedicineIdAsync(Guid medicineId, CancellationToken ct = default);

        // ৩. নির্দিষ্ট অর্ডারের মোট ডিসকাউন্ট ক্যালকুলেট করা
        Task<decimal> GetTotalDiscountByOrderIdAsync(Guid orderId, CancellationToken ct = default);

        // ৪. একটি অর্ডারে নির্দিষ্ট ওষুধটি আছে কি না চেক করা
        Task<MedicineOrderItem?> GetItemByOrderAndMedicineIdAsync(Guid orderId, Guid medicineId, CancellationToken ct = default);
    }
}
