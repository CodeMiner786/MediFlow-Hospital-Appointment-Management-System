using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.DoctorLeave;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorLeave : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid              DoctorId         { get; set; }
    public    DoctorEntity      Doctor           { get; set; } = null!;

    // ── ২. ছুটির সময় ও ধরন ──────────────────────────────────────────────────────
    public    DateTime          LeaveFrom        { get; set; }
    public    DateTime          LeaveTo          { get; set; }
    public    string            Reason           { get; set; } = string.Empty;
    public    DoctorLeaveType   LeaveType        { get; set; }

    // ── ৩. অ্যাপ্রুভাল স্ট্যাটাস ───────────────────────────────────────────────────
    public    bool              IsApproved       { get; set; } = false;
    public    DateTime?         ApprovedAt       { get; set; }
    public    string?           ApprovedBy       { get; set; }
    public    string?           RejectionReason  { get; set; }

    // ── ৪. সাধারণ নোটস ───────────────────────────────────────────────────────────
    public    string?           Notes            { get; set; }
}