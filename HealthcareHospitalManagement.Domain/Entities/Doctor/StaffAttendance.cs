using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class StaffAttendance : BaseEntity
{
    // ── ১. স্টাফ রিলেশন ──────────────────────────────────────────────────────────
    public    Guid              StaffId           { get; set; }
    public    StaffEntity       Staff             { get; set; } = null!;

    // ── ২. উপস্থিতির সময় ও তারিখ ──────────────────────────────────────────────────
    public    DateOnly          AttendanceDate    { get; set; }
    public    TimeOnly?         CheckInTime       { get; set; }
    public    TimeOnly?         CheckOutTime      { get; set; }

    // ── ৩. স্ট্যাটাস এবং নোটস ─────────────────────────────────────────────────────
    public    bool              IsPresent         { get; set; }
    public    bool              IsOnLeave         { get; set; }
    public    string?           LeaveReason       { get; set; }
    public    string?           Notes             { get; set; }
}