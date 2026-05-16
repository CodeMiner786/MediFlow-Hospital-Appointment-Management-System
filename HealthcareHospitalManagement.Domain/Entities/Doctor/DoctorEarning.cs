using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.DoctorEarning;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorEarning : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid              DoctorId                { get; set; }
    public    DoctorEntity      Doctor                  { get; set; } = null!;

    // ── ২. রেফারেন্স (Appointment/Telemedicine) ──────────────────────────────────
    public    Guid?             AppointmentId           { get; set; }
    public    Guid?             TelemedicineSessionId   { get; set; }

    // ── ৩. আয়ের হিসাব (Financials) ──────────────────────────────────────────────
    public    DateTime          EarningDate             { get; set; }
    public    decimal           TotalFee                { get; set; }
    public    decimal           HospitalSharePercent    { get; set; }
    public    decimal           HospitalShareAmount     { get; set; }
    public    decimal           DoctorShareAmount       { get; set; }

    // ── ৪. পেমেন্ট স্ট্যাটাস ───────────────────────────────────────────────────────
    public    bool              IsPaid                  { get; set; } = false;
    public    DateTime?         PaidAt                  { get; set; }
    public    string?           PaymentReference        { get; set; } // ট্রানজেকশন আইডি
    
    // ── ৫. মেটাডাটা ─────────────────────────────────────────────────────────────
    public    DoctorEarningType EarningType             { get; set; }
    public    string?           Notes                   { get; set; }
}