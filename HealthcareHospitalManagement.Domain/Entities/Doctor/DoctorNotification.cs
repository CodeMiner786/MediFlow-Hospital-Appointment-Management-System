using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Enums.NotificationPriority;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorNotification : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid                  DoctorId       { get; set; }
    public    DoctorEntity          Doctor         { get; set; } = null!;

    // ── ২. নোটিফিকেশন কন্টেন্ট ────────────────────────────────────────────────────
    public    string                Title          { get; set; } = string.Empty;
    public    string                Message        { get; set; } = string.Empty;
    public    NotificationCategory  Category       { get; set; } // উদা: Appointment, System, Report

    // ── ৩. রেফারেন্স (Deep Linking) ─────────────────────────────────────────────
    public    Guid?                 ReferenceId    { get; set; } // নির্দিষ্ট কোনো এনটিটির আইডি
    public    string?               ReferenceType  { get; set; } // উদা: "Appointment", "LabReport"

    // ── ৪. স্ট্যাটাস এবং প্রায়োরিটি ────────────────────────────────────────────────
    public    bool                  IsRead         { get; set; } = false;
    public    DateTime?             ReadAt         { get; set; }
    public    string?               ActionUrl      { get; set; } // ক্লিক করলে কোথায় যাবে
    public    bool                  IsUrgent       { get; set; } = false;
    public    NotificationPriorityTypes Priority   { get; set; } = NotificationPriorityTypes.Medium;

    // ── ৫. টাইমস্ট্যাম্পস ─────────────────────────────────────────────────────────
    public    DateTime              DeliveredAt    { get; set; } = DateTime.UtcNow;
    public    DateTime?             ExpiresAt      { get; set; } // নির্দিষ্ট সময় পর নোটিফিকেশন মুছে যাবে
}