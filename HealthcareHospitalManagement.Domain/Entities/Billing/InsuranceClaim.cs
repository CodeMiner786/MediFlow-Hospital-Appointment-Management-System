using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Domain.Entities.Billing;

public class InsuranceClaim : BaseEntity
{
    // ── ১. রিলেশন (One-to-One with Bill) ──────────────────────────────────────────
    public    Guid?                  BillId              { get; set; }
    public    Bill                  Bill                { get; set; } = null!;

    // ── ২. ইন্স্যুরেন্স ডিটেইলস ──────────────────────────────────────────────────
    public    string                ClaimNumber         { get; set; } = string.Empty;
    public    string                InsuranceProvider   { get; set; } = string.Empty;
    public    string                PolicyNumber        { get; set; } = string.Empty;
    public    string                PolicyHolderName    { get; set; } = string.Empty;

    // ── ৩. টাকার হিসাব (Financials) ──────────────────────────────────────────────
    public    decimal               ClaimAmount         { get; set; }
    public    decimal?              ApprovedAmount      { get; set; }
    public    decimal?              RejectedAmount      { get; set; }

    // ── ৪. স্ট্যাটাস এবং ট্র্যাকিং ───────────────────────────────────────────────
    public    InsuranceClaimStatus  Status              { get; set; } = InsuranceClaimStatus.Submitted;
    public    DateTime              SubmittedDate       { get; set; }
    public    DateTime?             ProcessedDate       { get; set; }

    // ── ৫. এক্সট্রা ইনফরমেশন ─────────────────────────────────────────────────────
    public    string?               RejectionReason     { get; set; }
    public    string?               DocumentUrls        { get; set; }
    public    string?               Notes               { get; set; }
}