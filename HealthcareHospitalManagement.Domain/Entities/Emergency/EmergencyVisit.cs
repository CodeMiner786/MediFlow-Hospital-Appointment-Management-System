using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Domain.Entities.Emergency
{
    public class EmergencyVisit : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ও ট্র্যাকিং ———
        public      string              EmergencyCode           { get; set; } = string.Empty;


        // ——— ২. পেশেন্ট ইনফরমেশন (Required) ———
        public      Guid                PatientId               { get; set; }

        public      Patient             Patient                 { get; set; } = null!;


        // ——— ৩. স্টাফ ও অ্যাম্বুলেন্স ডিটেইলস (Optional) ———
        public      Guid?               AttendingDoctorId       { get; set; }

        public      DoctorEntity?       AttendingDoctor         { get; set; }

        public      Guid?               AmbulanceBookingId      { get; set; }

        public      AmbulanceBooking?   AmbulanceBooking        { get; set; }


        // ——— ৪. টাইম ট্র্যাকিং ———
        public      DateTime            ArrivalTime             { get; set; }

        public      DateTime?           TriageTime              { get; set; }

        public      DateTime?           DischargeTime           { get; set; }

        public      DateTime?           TimeOfDeath             { get; set; }


        // ——— ৫. স্ট্যাটাস ও ক্লিনিক্যাল ডেটা ———
        public      EmergencyLevel      TriageLevel             { get; set; }

        public      EmergencyStatus     Status                  { get; set; } = EmergencyStatus.Arrived;

        public      string              ChiefComplaint          { get; set; } = string.Empty;

        public      string?             ModeOfArrival           { get; set; }

        public      bool                IsAmbulance             { get; set; }


        // ——— ৬. মেডিকেল অ্যাসেসমেন্ট ———
        public      string?             InitialAssessment       { get; set; }

        public      string?             Diagnosis               { get; set; }

        public      string?             TreatmentGiven          { get; set; }

        public      string?             Medications             { get; set; }


        // ——— ৭. ডিসচার্জ ও আউটকাম ———
        public      string?             DischargeNotes          { get; set; }

        public      bool                IsAdmitted              { get; set; }

        public      Guid?               AdmissionId             { get; set; }

        public      bool                IsTransferred           { get; set; }

        public      string?             TransferredTo           { get; set; }

        public      bool                IsDeceased              { get; set; }

        public      string?             DeathCause              { get; set; }
    }
}