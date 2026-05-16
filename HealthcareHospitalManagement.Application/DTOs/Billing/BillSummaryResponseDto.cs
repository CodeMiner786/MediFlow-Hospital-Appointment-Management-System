using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class BillSummaryResponseDto
{
    public Guid     Id              { get; set; }
    public string   BillNumber      { get; set; } = string.Empty;
    public string   PatientName     { get; set; } = string.Empty;
    public string   BillType        { get; set; } = string.Empty;
    public DateTime BillDate        { get; set; }
    public decimal  TotalAmount     { get; set; }
    public decimal  PaidAmount      { get; set; }
    public decimal  DueAmount       { get; set; }
    public string   PaymentStatus   { get; set; } = string.Empty;
    public bool     IsFinalized     { get; set; }
}
