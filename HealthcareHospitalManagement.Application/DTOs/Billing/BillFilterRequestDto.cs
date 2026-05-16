using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class BillFilterRequestDto
{
    public Guid?            PatientId       { get; set; }
    public PaymentStatus?   PaymentStatus   { get; set; }
    public BillType?        BillType        { get; set; }
    public DateTime?        DateFrom        { get; set; }
    public DateTime?        DateTo          { get; set; }
    public int              PageNumber      { get; set; } = 1;
    public int              PageSize        { get; set; } = 10;
}
