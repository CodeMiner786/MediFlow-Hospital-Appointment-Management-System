using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Payment;

public class RefundRequestDto
{
    public Guid     PaymentTransactionId { get; set; }
    public string   RefundReason         { get; set; } = string.Empty;
}
