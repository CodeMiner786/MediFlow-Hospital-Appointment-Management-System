using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Entities.Wards
{
    /// <summary>
    /// Normal users can book a bed directly from the app.
    /// Payment goes to PlatformWallet.
    /// </summary>
    public class BedBooking : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ———
        public      string              BookingCode             { get; set; } = string.Empty;


        // ——— ২. রিলেশনশিপ (Bed, Patient, User) ———
        public      Guid                BedId                   { get; set; }
        public      Bed                 Bed                     { get; set; } = null!;

        public      Guid?               PatientId               { get; set; }
        public      Patient?            Patient                 { get; set; }

        public      Guid                BookedByUserId          { get; set; }
        public      ApplicationUser     BookedByUser            { get; set; } = null!;


        // ——— ৩. বুকিং ডিটেইলস ———
        public      DateTime            CheckInDate             { get; set; }

        public      DateTime?           CheckOutDate            { get; set; }

        public      int                 DurationDays            { get; set; }

        public      decimal             TotalCharge             { get; set; }


        // ——— ৪. পেমেন্ট ও স্ট্যাটাস ———
        public      bool                IsPaid                  { get; set; } = false;

        public      BedStatus           BookingStatus           { get; set; } = BedStatus.Reserved;

        public      string?             SpecialRequests         { get; set; }

        public      string?             Notes                   { get; set; }
    }
}