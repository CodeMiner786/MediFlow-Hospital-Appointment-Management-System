using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Entities.Wards
{
    public class Ward : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ———
        public      string              WardName            { get; set; } = string.Empty;

        public      string              WardCode            { get; set; } = string.Empty;

        public      WardType            WardType            { get; set; }


        // ——— ২. লোকেশন ও ক্যাপাসিটি ———
        public      int                 FloorNumber         { get; set; }

        public      string?             Building            { get; set; }

        public      int                 TotalBeds           { get; set; }

        public      int                 AvailableBeds       { get; set; }


        // ——— ৩. অ্যাডমিনিস্ট্রেশন ———
        public      string?             Description         { get; set; }

        public      string?             InchargeNurseName   { get; set; }


        // ——— ৪. নেভিগেশন কালেকশন ———
        public      ICollection<Bed>    Beds                { get; set; } = [];
    }
}