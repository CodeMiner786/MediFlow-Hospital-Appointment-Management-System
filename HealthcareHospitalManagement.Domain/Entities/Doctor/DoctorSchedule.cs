using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorSchedule : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid              DoctorId            { get; set; }
    public    DoctorEntity      Doctor              { get; set; } = null!;

    // ── ২. শিডিউল ডিটেইলস ────────────────────────────────────────────────────────
    public    DayOfWeek         DayOfWeek           { get; set; }
    public    TimeOnly          StartTime           { get; set; }
    public    TimeOnly          EndTime             { get; set; }
    public    ShiftType         ShiftType           { get; set; } // Morning, Evening, Night
    
    // ── ৩. কনসালটেশন সেটিংস ─────────────────────────────────────────────────────
    public    int               MaxAppointments     { get; set; }
    public    int               SlotDurationMinutes { get; set; } = 15;
    public    bool              IsAvailable         { get; set; } = true;
    public    string?           Location            { get; set; } // চেম্বার বা রুম নাম্বার
    public    bool              IsTelemedicineSlot  { get; set; } = false;

    // ── ৪. ভ্যালিডিটি এবং ট্র্যাকিং ───────────────────────────────────────────────
    public    string            SetBy               { get; set; } = string.Empty;
    public    DateTime          EffectiveFrom       { get; set; }
    public    DateTime?         EffectiveTo         { get; set; }

    // ── ৫. নেভিগেশন (স্লটসমূহ) ───────────────────────────────────────────────────
    public ICollection<DoctorScheduleSlot> Slots    { get; set; } = [];
}