using HealthcareHospitalManagement.Domain.Enums.BillingPayment;


namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class BillDetailResponseDto : BillSummaryResponseDto
{
    public DateTime?        DueDate         { get; set; }
    public decimal          SubTotal        { get; set; }
    public decimal          DiscountAmount  { get; set; }
    public decimal          TaxAmount       { get; set; }
    public List<BillItemResponseDto> Items  { get; set; } = [];
}
