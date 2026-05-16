using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class Supplier : BaseEntity
    {
        // ——— ১. বেসিক ইনফরমেশন ———
        public      string              SupplierName            { get; set; } = string.Empty;

        public      string              SupplierCode            { get; set; } = string.Empty;

        public      string              ContactPerson           { get; set; } = string.Empty;


        // ——— ২. কন্টাক্ট ডিটেইলস ———
        public      string              PhoneNumber             { get; set; } = string.Empty;

        public      string              Email                   { get; set; } = string.Empty;

        public      string              Address                 { get; set; } = string.Empty;

        public      string?             Website                 { get; set; }


        // ——— ৩. লিগ্যাল ও স্ট্যাটাস ———
        public      string?             TaxId                   { get; set; }

        public      SupplierStatus      Status                  { get; set; } = SupplierStatus.Active;

        public      string?             Notes                   { get; set; }


        // ——— ৪. নেভিগেশন কালেকশন ———
        public      ICollection<MedicineStock> Stocks           { get; set; } = [];
    }
}