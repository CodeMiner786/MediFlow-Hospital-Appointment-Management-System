using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class BillItemRequestDto
{
    public string   Description { get; set; } = string.Empty;
    public decimal  UnitPrice   { get; set; }
    public int      Quantity    { get; set; } = 1;
}
