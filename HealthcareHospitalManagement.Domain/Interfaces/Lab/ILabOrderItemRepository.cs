using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Domain.Interfaces.Lab
{
    public interface ILabOrderItemRepository : IGenericRepository<LabOrderItem>
    {
        // ১. একটি নির্দিষ্ট অর্ডারের সব আইটেম (টেস্ট) দেখা
        Task<IEnumerable<LabOrderItem>> GetItemsByOrderIdAsync(Guid orderId, CancellationToken ct = default);

        // ২. বারকোড দিয়ে স্পেসিফিক টেস্ট আইটেম খুঁজে বের করা (Sample Scanning)
        Task<LabOrderItem?> GetByBarcodeAsync(string barcode, CancellationToken ct = default);

        // ৩. স্ট্যাটাস অনুযায়ী টেস্ট লিস্ট (যেমন: কয়টা স্যাম্পল কালেক্ট করা বাকি)
        Task<IEnumerable<LabOrderItem>> GetItemsByStatusAsync(LabTestStatus status, CancellationToken ct = default);

        // ৪. অস্বাভাবিক (Abnormal) রেজাল্ট আসা টেস্টগুলো দ্রুত খুঁজে বের করা
        Task<IEnumerable<LabOrderItem>> GetAbnormalResultsAsync(CancellationToken ct = default);

        // ৫. স্যাম্পল কালেকশন স্ট্যাটাস আপডেট করা
        Task UpdateSampleStatusAsync(Guid itemId, string collectedBy, string barcode, CancellationToken ct = default);

        // ৬. টেস্ট রেজাল্ট এবং রেফারেন্স রেঞ্জ আপডেট করা
        Task UpdateTestResultAsync(Guid itemId, string resultValue, bool isAbnormal, string enteredBy, CancellationToken ct = default);

        Task<IEnumerable<LabOrderItem>> GetByLabOrderIdAsync(Guid labOrderId, CancellationToken ct = default);
    }
}

