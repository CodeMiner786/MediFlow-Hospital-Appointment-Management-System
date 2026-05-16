using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface IPrescriptionItemRepository : IGenericRepository<PrescriptionItem>
    {
        // ১. একটি নির্দিষ্ট প্রেসক্রিপশনের সব ওষুধের তালিকা দেখা
        Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(Guid prescriptionId, CancellationToken ct = default);

        // ২. নির্দিষ্ট প্রেসক্রিপশনে এখনো যে ওষুধগুলো ফার্মেসি থেকে দেওয়া হয়নি
        Task<IEnumerable<PrescriptionItem>> GetPendingDispenseItemsAsync(Guid prescriptionId, CancellationToken ct = default);

        // ৩. নির্দিষ্ট কোনো মেডিসিন পেশেন্টকে কতবার প্রেসক্রাইব করা হয়েছে তা দেখা
        Task<IEnumerable<PrescriptionItem>> GetHistoryByMedicineIdAsync(Guid medicineId, CancellationToken ct = default);

        // ৪. ওষুধ প্রদানের স্ট্যাটাস আপডেট করা (Dispensed)
        Task MarkAsDispensedAsync(Guid itemId, CancellationToken ct = default);
    }
}
