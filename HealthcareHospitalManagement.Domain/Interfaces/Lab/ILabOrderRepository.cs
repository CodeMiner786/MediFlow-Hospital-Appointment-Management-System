using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Domain.Interfaces.Lab
{
    public interface ILabOrderRepository : IGenericRepository<LabOrder>
    {
        // ১. অর্ডার কোড (OrderCode) দিয়ে অর্ডারের সব ডিটেইলস দেখা
        Task<LabOrder?> GetByOrderCodeAsync(string orderCode);

        // ২. নির্দিষ্ট একজন পেশেন্টের সব ল্যাব অর্ডারের হিস্টোরি
        Task<IEnumerable<LabOrder>> GetOrdersByPatientIdAsync(Guid patientId);

        // ৩. নির্দিষ্ট একজন ডক্টরের রেকমেন্ড করা সব ল্যাব অর্ডার
        Task<IEnumerable<LabOrder>> GetOrdersByDoctorIdAsync(Guid doctorId);

        // ৪. স্ট্যাটাস অনুযায়ী ফিল্টার (যেমন: কোনগুলো পেইড বা আন-পেইড)
        Task<IEnumerable<LabOrder>> GetOrdersByPaymentStatusAsync(bool isPaid);

        // ৫. হাই প্রায়োরিটি (Urgent/Stat) টেস্টগুলো দ্রুত খুঁজে বের করা
        Task<IEnumerable<LabOrder>> GetUrgentOrdersAsync();

        // ৬. নির্দিষ্ট ডেট রেঞ্জে কতগুলো ল্যাব অর্ডার হয়েছে (Report)
        Task<IEnumerable<LabOrder>> GetOrdersByDateRangeAsync(DateTime start, DateTime end);

        // 7 নতুন — Single Order with full details
        Task<LabOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);

        // 8 নতুন — Patient এর Orders with full details
        Task<IEnumerable<LabOrder>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    

        // 9 . Filtered and paginated retrieval of lab orders based on various criteria
        Task<List<LabOrder>> GetFilteredOrdersAsync(
            Guid? patientId,
            Guid? doctorId,
            TestPriority? priority,
            bool? isPaid,
            DateTime? dateFrom,
            DateTime? dateTo,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default
            );
    }
}
