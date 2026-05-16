using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class Medicine : BaseEntity
    {
        // ——— ১. ফার্মেসি রেফারেন্স ———
        public      Guid                PharmacyProfileId       { get; set; }
        public      PharmacyProfile     PharmacyProfile         { get; set; } = null!;

        // ——— ২. বেসিক ইনফো ———
        public      string              MedicineName            { get; set; } = string.Empty;
        public      string              GenericName             { get; set; } = string.Empty;
        public      string              BrandName               { get; set; } = string.Empty;
        public      string              MedicineCode            { get; set; } = string.Empty;
        public      MedicineCategory    Category                { get; set; }
        public      string              Manufacturer            { get; set; } = string.Empty;
        public      string?             Description             { get; set; }

        // ——— ৩. স্পেসিফিকেশন ও প্রাইজ ———
        public      string              Strength                { get; set; } = string.Empty; // e.g., 500mg
        public      string              Unit                    { get; set; } = string.Empty; // e.g., Tablet, Syrup
        public      decimal             SellingPrice            { get; set; }
        public      bool                RequiresPrescription    { get; set; }

        // ——— ৪. ক্লিনিক্যাল নোটস ———
        public      string?             SideEffects             { get; set; }
        public      string?             Contraindications       { get; set; }
        public      string?             StorageConditions       { get; set; }
        public      string?             ImageUrl                { get; set; }

        // ——— ৫. নেভিগেশন কালেকশনস ———
        public      ICollection<MedicineStock>      Stocks              { get; set; } = [];
        public      ICollection<PrescriptionItem>   PrescriptionItems   { get; set; } = [];
        public      ICollection<MedicineOrderItem>  OrderItems          { get; set; } = [];
    }
}