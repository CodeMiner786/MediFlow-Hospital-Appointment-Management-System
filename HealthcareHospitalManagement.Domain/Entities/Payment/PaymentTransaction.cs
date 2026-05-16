using HealthcareHospitalManagement.Domain.Common.BaseModel;
// 👇 এই নেমস্পেসটি এনামগুলো পাওয়ার জন্য যোগ করা হলো
using HealthcareHospitalManagement.Domain.Enums.BillingPayment; 

namespace HealthcareHospitalManagement.Domain.Entities.Payment;

public class PaymentTransaction : BaseEntity
{
    // ——— ১. আইডেন্টিফায়ার ———
    public    Guid                BillId                { get; set; } 
    public    Guid?               InvoiceId             { get; set; }

    // ——— ২. পেমেন্ট ডিটেইলস ———
    public    string              PaymentCode           { get; set; } = string.Empty; 
    public    PaymentStatus       Status                { get; set; } = PaymentStatus.Pending;
    public    PaymentDestination  Destination           { get; set; } = PaymentDestination.PlatformWallet;

    public    decimal             Amount                { get; set; }
    public    PaymentMethod       PaymentMethod         { get; set; } 
    public    PaymentGateway?     Gateway               { get; set; } 
    public    DateTime            PaymentDate           { get; set; } = DateTime.UtcNow;

    // ——— ৩. গেটওয়ে ও রেফারেন্স ———
    public    string?             TransactionId         { get; set; } 
    public    string?             GatewayTransactionId  { get; set; } 
    public    string?             GatewayResponse       { get; set; } 
    public    string?             ReferenceNumber       { get; set; }

    // ——— ৪. রিফান্ড ট্র্যাকিং ———
    public    bool                IsRefunded            { get; set; } = false;
    public    DateTime?           RefundedAt            { get; set; }
    public    string?             RefundReason          { get; set; }
    public    string?             RefundTransactionId   { get; set; }

    // ——— ৫. মেটাডেটা ও রিমার্কস ———
    public    string              ReceivedBy            { get; set; } = string.Empty; 
    public    string?             Remarks               { get; set; }

    // ——— ৬. নেভিগেশন প্রপার্টি ———
    // দ্রষ্টব্য: PlatformWalletTransaction এনটিটি আপনার সিস্টেমে থাকতে হবে
    public ICollection<PlatformWalletTransaction> PlatformWalletTransactions { get; set; } = [];
}