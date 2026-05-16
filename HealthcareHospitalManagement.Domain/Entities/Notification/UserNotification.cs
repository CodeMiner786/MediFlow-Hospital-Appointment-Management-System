using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Enums.NotificationPriority;

namespace HealthcareHospitalManagement.Domain.Entities.Notification
{
    public class UserNotification : BaseEntity
    {
        // ——— ১. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ২. কন্টেন্ট (Multi-language) ———
        public      string              Title                   { get; set; } = string.Empty;

        public      string              TitleBn                 { get; set; } = string.Empty;

        public      string              Message                 { get; set; } = string.Empty;

        public      string              MessageBn               { get; set; } = string.Empty;


        // ——— ৩. ক্যাটাগরি ও প্রায়োরিটি ———
        public      NotificationCategory        Category        { get; set; }

        public      NotificationPriorityTypes   Priority        { get; set; } = NotificationPriorityTypes.Medium;


        // ——— ৪. অ্যাকশন ও রেফারেন্স ———
        public      string?             ActionUrl               { get; set; }

        public      string?             IconType                { get; set; }

        public      Guid?               ReferenceId             { get; set; }

        public      string?             ReferenceType           { get; set; }


        // ——— ৫. স্ট্যাটাস ও ট্র্যাকিং ———
        public      bool                IsRead                  { get; set; } = false;

        public      DateTime?           ReadAt                  { get; set; }

        public      bool                IsUrgent                { get; set; } = false;

        public      DateTime?           ExpiresAt               { get; set; }


        // ——— ৬. ডেলিভারি চ্যানেল ট্র্যাকিং ———
        public      bool                IsEmailSent             { get; set; } = false;

        public      DateTime?           EmailSentAt             { get; set; }

        public      bool                IsSmsSent               { get; set; } = false;

        public      DateTime?           SmsSentAt               { get; set; }

        public      bool                IsPushSent              { get; set; } = false;

        public      DateTime?           PushSentAt              { get; set; }
    }
}