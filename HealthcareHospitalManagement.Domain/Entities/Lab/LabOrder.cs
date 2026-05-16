using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Domain.Entities.Lab
{
    public class LabOrder : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ———
        public      string              OrderCode               { get; set; } = string.Empty;


        // ——— ২. রেফারেন্স (পেশেন্ট ও ডক্টর) ———
        public      Guid                PatientId               { get; set; }
        public      Patient             Patient                 { get; set; } = null!;

        public      Guid?               OrderedByDoctorId       { get; set; }
        public      DoctorEntity?       OrderedByDoctor         { get; set; }


        // ——— ৩. ল্যাব প্রোফাইল ও টাইমিং ———
        public      Guid                LabProfileId            { get; set; }
        public      LabProfile          LabProfile              { get; set; } = null!;

        public      DateTime            OrderDate               { get; set; } = DateTime.UtcNow;


        // ——— ৪. ক্লিনিকাল ডিটেইলস ———
        public      TestPriority        Priority                { get; set; } = TestPriority.Routine;

        public      string?             ClinicalNotes           { get; set; }

        public      string?             Diagnosis               { get; set; }

        public      bool                IsFasting               { get; set; }

        public      string?             SpecialInstructions     { get; set; }


        // ——— ৫. স্ট্যাটাস ও পেমেন্ট ———
        public      bool                IsSharedWithPatient     { get; set; } = true;

        public      bool                IsPaid                  { get; set; } = false;

        public      decimal             TotalAmount             { get; set; }


        // ——— ৬. কালেকশন ———
        public      ICollection<LabOrderItem>   Items           { get; set; } = [];
    }
}