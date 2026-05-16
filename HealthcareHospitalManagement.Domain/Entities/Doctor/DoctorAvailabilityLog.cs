using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorAvailabilityLog : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid                      DoctorId        { get; set; }
    public    DoctorEntity              Doctor          { get; set; } = null!;

    // ── ২. স্ট্যাটাস এবং ট্র্যাকিং ─────────────────────────────────────────────────
    public    DoctorAvailabilityStatus  Status          { get; set; }
    public    DateTime                  ChangedAt       { get; set; } = DateTime.UtcNow;
    public    string?                   ChangedBy       { get; set; } // অ্যাডমিন বা ইউজারের নাম/আইডি

    // ── ৩. অতিরিক্ত তথ্য ──────────────────────────────────────────────────────────
    public    string?                   Reason          { get; set; } // উদা: "Emergency Surgery"
    public    DateTime?                 ExpectedBackAt  { get; set; } // ফিরতে কতক্ষণ দেরি হতে পারে
}