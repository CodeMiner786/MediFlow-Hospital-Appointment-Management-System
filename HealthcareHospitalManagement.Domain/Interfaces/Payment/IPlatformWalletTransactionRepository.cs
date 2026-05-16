using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Payment
{
    public interface IPlatformWalletTransactionRepository : IGenericRepository<PlatformWalletTransaction>
    {
        // ১. একটি নির্দিষ্ট ওয়ালেটের সব ট্রানজেকশন হিস্টোরি দেখা
        Task<IEnumerable<PlatformWalletTransaction>> GetTransactionsByWalletIdAsync(Guid walletId);

        // ২. ট্রানজেকশন টাইপ অনুযায়ী ফিল্টার করা (যেমন: শুধু 'Refund' বা 'DoctorPayment' দেখা)
        Task<IEnumerable<PlatformWalletTransaction>> GetTransactionsByTypeAsync(string transactionType);

        // ৩. নির্দিষ্ট পেমেন্ট ট্রানজেকশন আইডি দিয়ে লেজার এন্ট্রি খুঁজে বের করা
        Task<PlatformWalletTransaction?> GetByPaymentIdAsync(Guid paymentId);

        // ৪. নির্দিষ্ট ডেট রেঞ্জে ট্রানজেকশন লিস্ট (Account Statement এর জন্য)
        Task<IEnumerable<PlatformWalletTransaction>> GetStatementByDateRangeAsync(DateTime start, DateTime end);

        // ৫. একটি নির্দিষ্ট ওয়ালেটের শেষ ট্রানজেকশনটি দেখা
        Task<PlatformWalletTransaction?> GetLatestTransactionAsync(Guid walletId);
    }
}
