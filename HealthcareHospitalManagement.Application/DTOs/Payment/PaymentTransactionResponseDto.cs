using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Payment;

public class PaymentTransactionResponseDto
{
    public Guid     Id                  { get; set; }
    public string   PaymentCode         { get; set; } = string.Empty;
    public Guid     BillId              { get; set; }
    public decimal  Amount              { get; set; }
    public string   PaymentMethod       { get; set; } = string.Empty;
    public string?  Gateway             { get; set; }
    public string   Status              { get; set; } = string.Empty;
    public string   Destination         { get; set; } = string.Empty;
    public DateTime PaymentDate         { get; set; }
    public string?  TransactionId       { get; set; }
    public string?  GatewayTransactionId { get; set; }
    public bool     IsRefunded          { get; set; }
    public DateTime? RefundedAt         { get; set; }
    public string   ReceivedBy          { get; set; } = string.Empty;
}
