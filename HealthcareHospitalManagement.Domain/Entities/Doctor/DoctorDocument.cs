using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorDocument : BaseEntity
{
    // ── ১. রিলেশন (Doctor) ────────────────────────────────────────────────────────
    public    Guid                  DoctorId        { get; set; }
    public    DoctorEntity          Doctor          { get; set; } = null!;

    // ── ২. ডকুমেন্ট ডিটেইলস ───────────────────────────────────────────────────────
    public    DoctorDocumentType    DocumentType    { get; set; }
    public    string                DocumentName    { get; set; } = string.Empty;
    public    string                FileUrl         { get; set; } = string.Empty;
    public    string?               FileType        { get; set; } // pdf, jpg, png
    public    long?                 FileSizeBytes   { get; set; }
    public    DateTime?             ExpiryDate      { get; set; } // সার্টিফিকেটের মেয়াদ থাকলে

    // ── ৩. ভেরিফিকেশন স্ট্যাটাস ────────────────────────────────────────────────────
    public    bool                  IsVerified      { get; set; } = false;
    public    DateTime?             VerifiedAt      { get; set; }
    public    string?               VerifiedBy      { get; set; } // অ্যাডমিনের নাম বা আইডি
    public    string?               Notes           { get; set; }
}