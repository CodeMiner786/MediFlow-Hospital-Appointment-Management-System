using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class InvoiceResponseDto
{
    public Guid     Id              { get; set; }
    public string   InvoiceNumber   { get; set; } = string.Empty;
    public string   PatientName     { get; set; } = string.Empty;
    public DateTime IssuedDate      { get; set; }
    public decimal  TotalAmount     { get; set; }
    public string   Status          { get; set; } = string.Empty;
}
