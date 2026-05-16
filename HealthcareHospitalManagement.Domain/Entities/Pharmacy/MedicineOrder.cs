using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class MedicineOrder : BaseEntity
    {
        // ——— ১. আইডেন্টিফায়ার ও পেশেন্ট ———
        public      string              OrderCode           { get; set; } = string.Empty;

        public      Guid                PatientId           { get; set; }
        public      Patient             Patient             { get; set; } = null!;


        // ——— ২. সোর্স (Optional Prescription) ———
        public      Guid?               PrescriptionId      { get; set; }
        public      Prescription?       Prescription        { get; set; }


        // ——— ৩. অর্ডার স্ট্যাটাস ও টাইমলাইন ———
        public      MedicineOrderStatus Status              { get; set; } = MedicineOrderStatus.Placed;

        public      DateTime            OrderDate           { get; set; } = DateTime.UtcNow;

        public      DateTime?           DeliveryDate        { get; set; }


        // ——— ৪. ফিনান্সিয়াল ও ডেলিভারি ডিটেইলস ———
        public      decimal             TotalAmount         { get; set; } // Set via Handler

        public      bool                IsPaid              { get; set; } = false;

        public      string?             DeliveryAddress     { get; set; }

        public      string?             Notes               { get; set; }


        // ——— ৫. নেভিগেশন ———
        public      ICollection<MedicineOrderItem> Items    { get; set; } = [];
    }
}