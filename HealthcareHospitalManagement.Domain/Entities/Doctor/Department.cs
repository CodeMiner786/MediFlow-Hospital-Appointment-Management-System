using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class Department : BaseEntity
{
    // ── ১. ডিপার্টমেন্ট বেসিকস ────────────────────────────────────────────────────
    public    string            Name              { get; set; } = string.Empty;
    public    string            Code              { get; set; } = string.Empty; // যেমন: CARD, NEURO
    public    string?           Description       { get; set; }
    
    // ── ২. অ্যাডমিনিস্ট্রেটিভ ডিটেইলস ─────────────────────────────────────────────
    public    string?           Location          { get; set; } // ফ্লোর নাম্বার বা বিল্ডিং
    public    string?           HeadDoctorName    { get; set; } // বিভাগীয় প্রধান
    public    string?           ContactExtension  { get; set; } // ইন্টারনাল ফোন নম্বর

    // ── ৩. কালেকশন (Relationships) ──────────────────────────────────────────────
    public    ICollection<DoctorEntity> Doctors   { get; set; } = [];
    public    ICollection<StaffEntity>  Staff     { get; set; } = [];
}