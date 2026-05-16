using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class BillItemResponseDto
{
    public Guid     Id          { get; set; }
    public string   Description { get; set; } = string.Empty;
    public decimal  UnitPrice   { get; set; }
    public int      Quantity    { get; set; }
    public decimal  TotalPrice  { get; set; }
}
