using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Domain.Interfaces.Payment
{
    public interface IPaymentTransactionRepository : IGenericRepository<PaymentTransaction>
    {
        // ১. ট্রানজেকশন আইডি (যেমন: SSL Commerz বা বিকাশ আইডি) দিয়ে খুঁজে বের করা
        Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId, CancellationToken ct = default);

        // ২. বিল আইডি (BillId) দিয়ে পেমেন্ট হিস্টোরি দেখা
        Task<IEnumerable<PaymentTransaction>> GetTransactionsByBillIdAsync(Guid billId, CancellationToken ct = default);

        // ৩. নির্দিষ্ট পেমেন্ট স্ট্যাটাস (Success/Failed/Pending) অনুযায়ী ডাটা দেখা
        Task<IEnumerable<PaymentTransaction>> GetTransactionsByStatusAsync(PaymentStatus status, CancellationToken ct = default);

        // ৪. পেমেন্ট গেটওয়ে অনুযায়ী ট্রানজেকশন রিপোর্ট (Bkash, SSLCommerz, Cash)
        Task<IEnumerable<PaymentTransaction>> GetTransactionsByGatewayAsync(PaymentGateway gateway, CancellationToken ct = default);

        // ৫. রিফান্ড স্ট্যাটাস আপডেট করা (অডিট ট্রেইল সহ)
        Task UpdateRefundStatusAsync(Guid transactionId, string reason, string refundTxnId, CancellationToken ct = default);

        // ৬. নির্দিষ্ট সময়ের মধ্যে মোট রেভিনিউ ক্যালকুলেট করা
        Task<decimal> GetTotalRevenueByDateRangeAsync(DateTime start, DateTime end, CancellationToken ct = default);
    }
}
