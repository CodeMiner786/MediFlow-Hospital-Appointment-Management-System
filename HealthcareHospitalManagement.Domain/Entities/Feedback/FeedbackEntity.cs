using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.FeedbackRating;

namespace HealthcareHospitalManagement.Domain.Entities.Feedback
{
    public class FeedbackEntity : BaseEntity
    {
        // ——— ১. পেশেন্ট রেফারেন্স ———
        public      Guid                PatientId               { get; set; }

        public      Patient             Patient                 { get; set; } = null!;


        // ——— ২. ফিডব্যাক টাইপ ও স্ট্যাটাস ———
        public      FeedbackType        FeedbackType            { get; set; }

        public      FeedbackStatus      Status                  { get; set; } = FeedbackStatus.Pending;


        // ——— ৩. টার্গেট রেফারেন্স (ঐচ্ছিক) ———
        public      Guid?               DoctorId                { get; set; }

        public      Guid?               AmbulanceProviderId     { get; set; }

        public      Guid?               LabProfileId            { get; set; }

        public      Guid?               PharmacyProfileId       { get; set; }


        // ——— ৪. রেটিং ও কন্টেন্ট ———
        public      int                 Rating                  { get; set; } // 1–5

        public      string?             Comment                 { get; set; }

        public      bool                IsAnonymous             { get; set; } = false;


        // ——— ৫. এডমিন রেসপন্স ———
        public      string?             AdminResponse           { get; set; }

        public      DateTime?           RespondedAt             { get; set; }

        public      string?             RespondedBy             { get; set; }

        public      bool                IsPublished             { get; set; } = false;
    }
}