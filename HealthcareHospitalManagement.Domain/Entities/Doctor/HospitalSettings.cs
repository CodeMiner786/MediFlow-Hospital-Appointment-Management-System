using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class HospitalSettings : BaseEntity
{
    // ── ১. রেভিনিউ শেয়ারিং সেটিংস (Financials) ───────────────────────────────────
    /// <summary>প্লাটফর্ম বা হাসপাতাল কত শতাংশ কমিশন পাবে (Default 20%)।</summary>
    public    decimal    DefaultPlatformSharePercent    { get; set; } = 20;

    /// <summary>ডাক্তার কত শতাংশ শেয়ার পাবেন (Default 80%)।</summary>
    public    decimal    DefaultDoctorSharePercent      { get; set; } = 80;
}