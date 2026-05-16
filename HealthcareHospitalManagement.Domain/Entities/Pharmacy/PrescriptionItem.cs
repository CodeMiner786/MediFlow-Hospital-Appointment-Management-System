using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class PrescriptionItem : BaseEntity
    {
        // ——— ১. রিলেশনস (Prescription & Medicine) ———
        public      Guid                PrescriptionId          { get; set; }
        public      Prescription        Prescription            { get; set; } = null!;

        public      Guid                MedicineId              { get; set; }
        public      Medicine            Medicine                { get; set; } = null!;


        // ——— ২. ডোজ ও ইন্সট্রাকশন ———
        public      string              Dosage                  { get; set; } = string.Empty; // e.g., 500mg

        public      string              Frequency               { get; set; } = string.Empty; // e.g., 1+0+1

        public      string              Route                   { get; set; } = string.Empty; // e.g., Oral

        public      int                 DurationDays            { get; set; }

        public      int                 Quantity                { get; set; }

        public      bool                WithFood                { get; set; }

        public      string?             SpecialInstructions     { get; set; }


        // ——— ৩. ফার্মেসি স্ট্যাটাস ———
        public      bool                IsDispensed             { get; set; }

        public      DateTime?           DispensedAt             { get; set; }
    }
}