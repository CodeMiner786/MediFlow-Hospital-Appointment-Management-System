using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Billing;

public class BillItem : BaseEntity
{
    // ── ১. রিলেশন ─────────────────────────────────────────────────────────────
    public    Guid      BillId          { get; set; }
    public    Bill      Bill            { get; set; } = null!;

    // ── ২. সার্ভিস ডিটেইলস ──────────────────────────────────────────────────────
    public    string    ServiceName     { get; set; } = string.Empty; 
    public    string?   ServiceCode     { get; set; }                 
    public    string?   Description     { get; set; }                 

   
    public    int       Quantity        { get; set; } 
    public    decimal   UnitPrice       { get; set; } 
    public    decimal   Discount        { get; set; } 
    public    decimal   TotalPrice      { get; set; } 
}