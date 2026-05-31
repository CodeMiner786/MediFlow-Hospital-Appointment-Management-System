using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Patient;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class StaffEntity : BaseEntity
{
    // ── ১. আইডেন্টিটি এবং পার্সোনাল ইনফো ──────────────────────────────────────────
    public    Guid              ApplicationUserId   { get; set; }
    public    string            FirstName           { get; set; } = string.Empty;
    public    string            LastName            { get; set; } = string.Empty;
    public    string            FullName            => $"{FirstName} {LastName}";
    public    DateTime          DateOfBirth         { get; set; }
    public    Gender            Gender              { get; set; }
    public    string?           ProfileImageUrl     { get; set; }

    // ── ২. প্রফেশনাল এবং স্যালারি ডিটেইলস ─────────────────────────────────────────
    public    string            StaffCode           { get; set; } = string.Empty; // উদা: STAFF-101
    public    StaffType         StaffType           { get; set; } // Nurse, Receptionist, etc.
    public    string            Designation         { get; set; } = string.Empty;
    public    string            Qualifications      { get; set; } = string.Empty;
    public    DateTime          JoiningDate         { get; set; }
    public    decimal           Salary              { get; set; }
    public    ShiftType         ShiftType           { get; set; }

    // ── ৩. কন্টাক্ট এবং লিগ্যাল ইনফো ──────────────────────────────────────────────
    public    string            PhoneNumber         { get; set; } = string.Empty;
    public    string            Email               { get; set; } = string.Empty;
    public    string            Address             { get; set; } = string.Empty;
    public    string            NationalId          { get; set; } = string.Empty;

    // ── ৪. ডিপার্টমেন্ট এবং এটেনডেন্স রিলেশন ──────────────────────────────────────
    public    Guid              DepartmentId        { get; set; }
    public    DepartmentEntity        Department          { get; set; } = null!;

    public    ICollection<StaffAttendance> Attendances { get; set; } = [];
}