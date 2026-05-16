using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Patients;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorNote : BaseEntity
{
    // ── ১. ডাক্তার ও পেশেন্ট রিলেশন ───────────────────────────────────────────────
    public    Guid          DoctorId        { get; set; }
    public    DoctorEntity  Doctor          { get; set; } = null!;

    public    Guid          PatientId       { get; set; }
    public    Patient       Patient         { get; set; } = null!;

    // ── ২. অ্যাপয়েন্টমেন্ট রেফারেন্স ──────────────────────────────────────────────
    public    Guid?         AppointmentId   { get; set; }

    // ── ৩. নোট কন্টেন্ট ─────────────────────────────────────────────────────────
    public    string        NoteTitle       { get; set; } = string.Empty;
    public    string        NoteContent     { get; set; } = string.Empty;
    
    // ── ৪. সেটিংস ও মেটাডাটা ──────────────────────────────────────────────────────
    public    bool          IsPrivate       { get; set; } = false; // শুধুমাত্র ডাক্তার দেখতে পাবেন
    public    bool          IsPinned        { get; set; } = false; // লিস্টের উপরে থাকবে কি না
    public    string?       Tags            { get; set; } // উদা: ["Critical", "Follow-up"]
}