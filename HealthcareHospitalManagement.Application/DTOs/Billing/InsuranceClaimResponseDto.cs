using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class InsuranceClaimResponseDto
{
    public Guid     Id              { get; set; }
    public string   ClaimNumber     { get; set; } = string.Empty;
    public string   InsuranceProvider { get; set; } = string.Empty;
    public decimal  ClaimedAmount   { get; set; }
    public decimal? ApprovedAmount  { get; set; }
    public string   Status          { get; set; } = string.Empty;
    public DateTime SubmittedAt     { get; set; }
}
