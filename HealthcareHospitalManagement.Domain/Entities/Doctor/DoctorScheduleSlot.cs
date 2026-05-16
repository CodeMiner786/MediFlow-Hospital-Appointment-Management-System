using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorScheduleSlot : BaseEntity
{
    // ── ১. রিলেশনশিপ (Schedule & Doctor) ──────────────────────────────────────────
    public    Guid              DoctorScheduleId   { get; set; }
    public    DoctorSchedule    DoctorSchedule     { get; set; } = null!;

    public    Guid              DoctorId           { get; set; }
    public    DoctorEntity      Doctor             { get; set; } = null!;

    // ── ২. স্লট টাইম এবং তারিখ ─────────────────────────────────────────────────────
    public    DateTime          SlotDate           { get; set; }
    public    TimeOnly          SlotStartTime      { get; set; }
    public    TimeOnly          SlotEndTime        { get; set; }

    // ── ৩. স্লট স্ট্যাটাস এবং সেটিংস ──────────────────────────────────────────────────
    public    bool              IsBooked           { get; set; } = false;
    public    bool              IsBlocked          { get; set; } = false;
    public    string?           BlockReason        { get; set; }
    public    bool              IsTelemedicine     { get; set; } = false;
    
    // ── ৪. অ্যাপয়েন্টমেন্ট রেফারেন্স ──────────────────────────────────────────────
    public    Guid?             AppointmentId      { get; set; }
}