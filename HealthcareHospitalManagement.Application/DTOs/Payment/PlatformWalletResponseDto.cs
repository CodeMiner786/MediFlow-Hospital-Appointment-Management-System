using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Payment;

public class PlatformWalletResponseDto
{
    public Guid     Id              { get; set; }
    public decimal  Balance         { get; set; }
    public decimal  TotalEarned     { get; set; }
    public decimal  TotalWithdrawn  { get; set; }
    public DateTime LastUpdatedAt   { get; set; }
}
