using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Entities.Wards
{
    public class Bed : BaseEntity
    {
        // ——— ১. বেসিক বেড ইনফরমেশন ———
        public      string              BedNumber           { get; set; } = string.Empty;

        public      BedStatus           Status              { get; set; } = BedStatus.Available;

        public      decimal             DailyCharge         { get; set; }

        public      string?             Notes               { get; set; }


        // ——— ২. ফ্যাসিলিটিজ ও ক্যাটাগরি ———
        public      bool                HasOxygen           { get; set; }

        public      bool                HasMonitor          { get; set; }

        public      bool                IsIsolation         { get; set; }


        // ——— ৩. রিলেশনস (Ward) ———
        public      Guid                WardId              { get; set; }
        public      Ward                Ward                { get; set; } = null!;


        // ——— ৪. নেভিগেশন কালেকশনস ———
        public      ICollection<Admission>  Admissions      { get; set; } = [];

        public      ICollection<BedBooking> Bookings        { get; set; } = [];
    }
}