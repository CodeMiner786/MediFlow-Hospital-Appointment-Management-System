using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Domain.Entities.Patients 
{
    public class Patient : BaseEntity
    {
        // ——— ১. আইডেন্টিটি ও বেসিক ইনফো ———
        public      Guid                ApplicationUserId       { get; set; }
        public      string              PatientCode             { get; set; } = string.Empty;
        public      string              FirstName               { get; set; } = string.Empty;
        public      string              LastName                { get; set; } = string.Empty;
        public      DateTime            DateOfBirth             { get; set; }
        public      int                 Age                     { get; set; } 
        public      Gender              Gender                  { get; set; }
        public      BloodGroup          BloodGroup              { get; set; } = BloodGroup.Unknown;
        public      MaritalStatus       MaritalStatus           { get; set; }
        public      string?             ProfileImageUrl         { get; set; }
        public      string              NationalId              { get; set; } = string.Empty;
        public      PatientType         PatientType             { get; set; } = PatientType.General;

        // ——— ২. কন্টাক্ট ও লোকেশন ———
        public      string              PhoneNumber             { get; set; } = string.Empty;
        public      string?             AlternatePhone          { get; set; }
        public      string              Email                   { get; set; } = string.Empty;
        public      string              Address                 { get; set; } = string.Empty;
        public      string              City                    { get; set; } = string.Empty;
        public      string              State                   { get; set; } = string.Empty;
        public      string              ZipCode                 { get; set; } = string.Empty;
        public      string              Country                 { get; set; } = string.Empty;

        // ——— ৩. ইমার্জেন্সি কন্টাক্ট ———
        public      string              EmergencyContactName    { get; set; } = string.Empty;
        public      string              EmergencyContactPhone   { get; set; } = string.Empty;
        public      string              EmergencyContactRelation { get; set; } = string.Empty;

        // ——— ৪. ফিজিক্যাল স্ট্যাটাস ———
        public      decimal?            WeightKg                { get; set; }
        public      decimal?            HeightCm                { get; set; }
        public      decimal?            BMI                     { get; set; } 

        // ——— ৫. মেডিক্যাল হিস্ট্রি ———
        public      string?             Allergies               { get; set; }
        public      string?             ChronicDiseases         { get; set; }
        public      string?             CurrentMedications      { get; set; }
        public      string?             PastSurgeries           { get; set; }
        public      string?             FamilyMedicalHistory    { get; set; }
        public      string?             Notes                   { get; set; }

        // ——— ৬. ইনস্যুরেন্স ———
        public      bool                HasInsurance            { get; set; }
        public      string?             InsuranceProvider       { get; set; }
        public      string?             InsurancePolicyNumber   { get; set; }
        public      DateTime?           InsuranceExpiryDate     { get; set; }

        // ——— ৭. নেভিগেশন প্রপার্টিজ (Fixing the 'Vitals' error) ———
        
        // এটি আপনার এরর ফিক্স করবে
        public      ICollection<PatientVital>       Vitals          { get; set; } = []; 
        
        public      ICollection<HealthLog>          HealthLogs      { get; set; } = [];
        public      ICollection<MedicalRecord>      MedicalRecords  { get; set; } = [];
        public      ICollection<AppointmentEntity>  Appointments    { get; set; } = [];
        public      ICollection<LabOrder>           LabOrders       { get; set; } = [];
        public      ICollection<Bill>               Bills           { get; set; } = [];
        public      ICollection<Admission>          Admissions      { get; set; } = [];
        public      ICollection<EmergencyVisit>     EmergencyVisits { get; set; } = [];
    }
}