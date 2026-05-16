using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Entities.Patients
{
    public class MedicalRecord : BaseEntity
    {
        // ——— ১. পেশেন্ট ও ডক্টর রেফারেন্স ———
        public      Guid                PatientId               { get; set; }
        public      Patient             Patient                 { get; set; } = null!;

        public      Guid                DoctorId                { get; set; }
        public      DoctorEntity        Doctor                  { get; set; } = null!;


        // ——— ২. ভিজিট ও ক্লিনিকাল ডিটেইলস ———
        public      DateTime            VisitDate               { get; set; } = DateTime.UtcNow;

        public      string              ChiefComplaint          { get; set; } = string.Empty; // প্রধান সমস্যা

        public      string              Diagnosis               { get; set; } = string.Empty; // রোগ নির্ণয়

        public      string?             DifferentialDiagnosis   { get; set; } // সম্ভাব্য অন্যান্য রোগ


        // ——— ৩. ট্রিটমেন্ট ও নোটস ———
        public      string?             TreatmentPlan           { get; set; }

        public      string?             Prescription            { get; set; }

        public      string?             ClinicalNotes           { get; set; }


        // ——— ৪. রেফারেল ও ফলো-আপ ———
        public      string?             ReferralTo              { get; set; }

        public      DateTime?           FollowUpDate            { get; set; }


        // ——— ৫. ফাইল ও প্রাইভেসী ———
        public      string?             AttachmentUrls          { get; set; } // JSON array of URLs (Reports/Scans)

        public      bool                IsSharedWithPatient     { get; set; } = true;
    }
}