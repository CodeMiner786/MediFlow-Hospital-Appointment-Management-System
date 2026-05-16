using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorUnavailability : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid              DoctorId           { get; set; }
    public    DoctorEntity      Doctor             { get; set; } = null!;

    // ── ২. অনুপস্থিতির সময় ও তারিখ ────────────────────────────────────────────────
    public    DateTime          UnavailableDate    { get; set; }
    public    TimeOnly?         FromTime           { get; set; }
    public    TimeOnly?         ToTime             { get; set; }
    
    // ── ৩. অতিরিক্ত তথ্য ──────────────────────────────────────────────────────────
    public    string?           Reason             { get; set; }
    public    bool              IsFullDay          { get; set; } = false;
}