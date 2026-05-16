using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class MedicineStock : BaseEntity
    {
        // ——— ১. রেফারেন্স (Medicine & Supplier) ———
        public      Guid                MedicineId              { get; set; }
        public      Medicine            Medicine                { get; set; } = null!;

        public      Guid?               SupplierId              { get; set; }
        public      Supplier?           Supplier                { get; set; }


        // ——— ২. স্টক ও ব্যাচ ডিটেইলস ———
        public      string              BatchNumber             { get; set; } = string.Empty;
        public      int                 QuantityInStock         { get; set; }
        public      string?             StorageLocation         { get; set; }


        // ——— ৩. রি-অর্ডার লজিক ———
        public      int                 ReorderLevel            { get; set; }
        public      int                 ReorderQuantity         { get; set; }


        // ——— ৪. প্রাইজ ও ডেট ———
        public      decimal             PurchasePrice           { get; set; }
        public      decimal             SellingPrice            { get; set; }

        public      DateTime            ManufactureDate         { get; set; }
        public      DateTime            ExpiryDate              { get; set; }


        // ——— ৫. স্ট্যাটাস ———
        public      StockStatus         StockStatus             { get; set; }
    }
}