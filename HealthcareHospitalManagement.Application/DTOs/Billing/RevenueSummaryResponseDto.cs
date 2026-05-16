using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class RevenueSummaryResponseDto
{
    public decimal  TotalRevenue        { get; set; }
    public decimal  PendingDues         { get; set; }
    public int      TotalBills          { get; set; }
    public int      PaidBills           { get; set; }
    public int      UnpaidBills         { get; set; }
    public DateTime DateFrom            { get; set; }
    public DateTime DateTo              { get; set; }
}
