using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Entities.Patients;

public class PatientReferral : BaseEntity
{
    // ── ১. রেফারেল কোড এবং মূল রিলেশন ──────────────────────────────────────────
    public    string            ReferralCode           { get; set; } = string.Empty;

    public    Guid              ReferringDoctorId      { get; set; } // যে ডাক্তার রেফার করছেন
    public    DoctorEntity      ReferringDoctor        { get; set; } = null!;

    public    Guid              PatientId              { get; set; }
    public    Patient           Patient                { get; set; } = null!;

    // ── ২. গন্তব্য (কার কাছে রেফার করা হচ্ছে) ──────────────────────────────────
    public    Guid?             ReferredToDoctorId     { get; set; } // নির্দিষ্ট ডাক্তার হলে
    public    DoctorEntity?     ReferredToDoctor       { get; set; }
    
    public    string?           ReferredToDepartment   { get; set; }
    public    string?           ReferredToHospital     { get; set; }

    // ── ৩. রেফারেল ডিটেইলস ──────────────────────────────────────────────────────
    public    DateTime          ReferralDate           { get; set; }
    public    string            Reason                 { get; set; } = string.Empty;
    public    string?           ClinicalSummary        { get; set; }
    public    string?           UrgencyLevel           { get; set; } // Low, Medium, High, Emergency

    // ── ৪. স্ট্যাটাস এবং নোটস ─────────────────────────────────────────────────────
    public    bool              IsAccepted             { get; set; } = false;
    public    DateTime?         AcceptedAt             { get; set; }
    public    string?           Notes                  { get; set; }
}