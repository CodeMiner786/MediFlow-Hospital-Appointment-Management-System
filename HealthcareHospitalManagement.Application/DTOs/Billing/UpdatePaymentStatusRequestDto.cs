using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Billing;

public class UpdatePaymentStatusRequestDto
{
    public Guid             BillId          { get; set; }
    public decimal          AmountPaid      { get; set; }
    public PaymentStatus    PaymentStatus   { get; set; }
}
