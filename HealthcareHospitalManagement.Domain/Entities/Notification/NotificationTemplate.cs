using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Notification;

namespace HealthcareHospitalManagement.Domain.Entities.Notification
{
    public class NotificationTemplate : BaseEntity
    {
        // ——— ১. টেমপ্লেট আইডেন্টিফিকেশন ———
        public      string              TemplateCode            { get; set; } = string.Empty; // e.g., OTP_VERIFICATION

        public      string              Name                    { get; set; } = string.Empty; // e.g., OTP Verification Email


        // ——— ২. চ্যানেল ও ক্যাটাগরি ———
        public      NotificationChannel     Channel             { get; set; } // Email, SMS, Push

        public      NotificationCategory    Category            { get; set; } // Security, Billing, System


        // ——— ৩. কন্টেন্ট (English) ———
        public      string              SubjectEn               { get; set; } = string.Empty;

        public      string              BodyEn                  { get; set; } = string.Empty;


        // ——— ৪. কন্টেন্ট (Bengali) ———
        public      string              SubjectBn               { get; set; } = string.Empty;

        public      string              BodyBn                  { get; set; } = string.Empty;


        // ——— ৫. স্ট্যাটাস ———
        public      bool                IsActive                { get; set; } = true;
    }
}