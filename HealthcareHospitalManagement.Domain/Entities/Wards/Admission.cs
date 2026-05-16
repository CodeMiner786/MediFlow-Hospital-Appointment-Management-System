using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Entities.Wards
{
    public class Admission : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ও কোর রিলেশনস ———
        public      string              AdmissionCode           { get; set; } = string.Empty;

        public      Guid                PatientId               { get; set; }
        public      Patient             Patient                 { get; set; } = null!;

        public      Guid                BedId                   { get; set; }
        public      Bed                 Bed                     { get; set; } = null!;

        public      Guid                AdmittingDoctorId       { get; set; }
        public      DoctorEntity        AdmittingDoctor         { get; set; } = null!;


        // ——— ২. অ্যাডমিশন ও ডিসচার্জ ডিটেইলস ———
        public      DateTime            AdmissionDate           { get; set; }

        public      DateTime?           DischargeDate           { get; set; }

        public      AdmissionStatus     Status                  { get; set; } = AdmissionStatus.Admitted;

        public      string              AdmissionReason         { get; set; } = string.Empty;

        public      string?             Diagnosis               { get; set; }

        public      string?             DischargeNotes          { get; set; }

        public      string?             DischargeSummary        { get; set; }


        // ——— ৩. ট্রান্সফার ইনফরমেশন ———
        public      bool                IsTransferred           { get; set; }

        public      string?             TransferredFrom         { get; set; }

        public      string?             TransferredTo           { get; set; }

        public      string?             TransferReason          { get; set; }
    }
}