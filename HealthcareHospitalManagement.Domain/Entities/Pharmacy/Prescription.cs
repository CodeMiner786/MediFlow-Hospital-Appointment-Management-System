using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class Prescription : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ———
        public      string              PrescriptionCode        { get; set; } = string.Empty;


        // ——— ২. রিলেশনস (Patient & Doctor) ———
        public      Guid                PatientId               { get; set; }
        public      Patient             Patient                 { get; set; } = null!;

        public      Guid                DoctorId                { get; set; }
        public      DoctorEntity        Doctor                  { get; set; } = null!;


        // ——— ৩. প্রেসক্রিপশন ডিটেইলস ———
        public      DateTime            PrescribedDate          { get; set; } = DateTime.UtcNow;

        public      DateTime?           ValidUntil              { get; set; }

        public      PrescriptionStatus  Status                  { get; set; } = PrescriptionStatus.Pending;

        public      string?             Diagnosis               { get; set; }

        public      string?             Notes                   { get; set; }


        // ——— ৪. রিফিল ও শেয়ারিং ———
        public      bool                IsRefillable            { get; set; }

        public      int                 RefillCount             { get; set; }

        public      bool                IsSharedWithPatient     { get; set; } = true;


        // ——— ৫. নেভিগেশন কালেকশন ———
        public      ICollection<PrescriptionItem> Items         { get; set; } = [];
    }
}