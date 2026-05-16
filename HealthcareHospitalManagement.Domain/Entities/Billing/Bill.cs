using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Domain.Entities.Billing;

public class Bill : BaseEntity
{
    // ── Identification ──────────────────────────────────────────────────────────
    public    string          BillNumber      { get; set; } = string.Empty; 
    public    BillType        BillType        { get; set; } 
    public    DateTime        BillDate        { get; set; } = DateTime.UtcNow;
    public    DateTime?       DueDate         { get; set; }
    public    bool            IsFinalized     { get; set; } = false; 

    // ── Foreign Keys & Relations ────────────────────────────────────────────────
    public    Guid            PatientId       { get; set; }
    public    Patient         Patient         { get; set; } = null!; 

    public    Guid?           InvoiceId       { get; set; }
    public    Invoice?        Invoice         { get; set; } 

    // ── Financials ──────────────────────────────────────────────────────────────
    public    decimal         SubTotal        { get; set; }      
    public    decimal         DiscountAmount  { get; set; }
    public    decimal         TaxAmount       { get; set; }     
    public    decimal         TotalAmount     { get; set; }   
    public    decimal         PaidAmount      { get; set; }    
    public    decimal         DueAmount       => TotalAmount - PaidAmount; 

    // ── Status & Collections ────────────────────────────────────────────────────
    public    PaymentStatus   PaymentStatus   { get; set; } = PaymentStatus.Pending;

    public    ICollection<BillItem>           Items    { get; set; } = []; 
    public    ICollection<PaymentTransaction> Payments { get; set; } = []; 

    public    InsuranceClaim? InsuranceClaim  { get; set; } 
}