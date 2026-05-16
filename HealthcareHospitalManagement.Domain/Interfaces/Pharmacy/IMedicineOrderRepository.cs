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
    public interface IMedicineOrderRepository : IGenericRepository<MedicineOrder>
    {
        // ১. অর্ডার কোড (e.g., ORD-1023) দিয়ে অর্ডার খুঁজে বের করা
        Task<MedicineOrder?> GetByOrderCodeAsync(string orderCode, CancellationToken ct);

        // ২. নির্দিষ্ট একজন পেশেন্টের সব ওষুধের অর্ডারের হিস্টোরি
        Task<IEnumerable<MedicineOrder>> GetOrdersByPatientIdAsync(Guid patientId, CancellationToken ct = default);

        // ৩. নির্দিষ্ট প্রেসক্রিপশনের বিপরীতে কোনো অর্ডার হয়েছে কি না তা দেখা
        Task<MedicineOrder?> GetOrderByPrescriptionIdAsync(Guid prescriptionId , CancellationToken ct = default);

        // ৪. অর্ডারের স্ট্যাটাস অনুযায়ী ফিল্টার (Pending, Shipped, Delivered)
        Task<IEnumerable<MedicineOrder>> GetOrdersByStatusAsync(MedicineOrderStatus status, CancellationToken ct = default);

        // ৫. বকেয়া বা পেইড অর্ডার আলাদা করা
        Task<IEnumerable<MedicineOrder>> GetOrdersByPaymentStatusAsync(bool isPaid, CancellationToken ct = default);

        // ৬. আজকের সব অর্ডারের লিস্ট (Daily Sales Report এর জন্য)
        Task<IEnumerable<MedicineOrder>> GetTodaysOrdersAsync(CancellationToken ct = default);
    }
}
