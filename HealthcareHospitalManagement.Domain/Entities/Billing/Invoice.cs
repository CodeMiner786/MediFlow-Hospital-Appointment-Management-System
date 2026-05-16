using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Domain.Entities.Billing;

public class Invoice : BaseEntity
{
    //  ইনভয়েস আইডেন্টিফিকেশন ──────────────────────────────────────────────
    public    string          InvoiceNumber   { get; set; } = string.Empty;

    //  ফরেন কি এবং রিলেশন ──────────────────────────────────────────────────
    public    Guid            PatientId       { get; set; }
    public    Patient         Patient         { get; set; } = null!;

    //  স্ট্যাটাস এবং ডেট ────────────────────────────────────────────────────
    public    InvoiceStatus   Status          { get; set; } = InvoiceStatus.Draft;
    public    DateTime        IssuedDate      { get; set; }
    public    DateTime?       DueDate         { get; set; }

    //  টাকা এবং ডকুমেন্ট ────────────────────────────────────────────────────
    public    decimal         TotalAmount     { get; set; }
    public    string?         PdfUrl          { get; set; }
    public    string?         Notes           { get; set; }
}