using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Payment;

public class CreatePaymentRequestDto
{
    public Guid             BillId          { get; set; }
    public Guid?            InvoiceId       { get; set; }
    public decimal          Amount          { get; set; }
    public PaymentMethod    PaymentMethod   { get; set; }
    public PaymentGateway?  Gateway         { get; set; }
    public string           ReceivedBy      { get; set; } = string.Empty;
    public string?          Remarks         { get; set; }
}
