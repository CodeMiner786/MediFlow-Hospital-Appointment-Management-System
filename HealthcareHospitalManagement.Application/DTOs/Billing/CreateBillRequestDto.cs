using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class CreateBillRequestDto
{
    public Guid             PatientId       { get; set; }
    public BillType         BillType        { get; set; }
    public DateTime?        DueDate         { get; set; }
    public decimal          DiscountAmount  { get; set; } = 0;
    public List<BillItemRequestDto> Items   { get; set; } = [];
}
