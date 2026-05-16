using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Payment
{
    public interface IPlatformWalletRepository : IGenericRepository<PlatformWallet>
    {
        // ১. প্ল্যাটফর্মের একমাত্র মূল ওয়ালেটটি খুঁজে বের করা (Singleton approach)
        Task<PlatformWallet?> GetMainWalletAsync();

        // ২. ওয়ালেটের ব্যালেন্স আপডেট করা (পেমেন্ট বা রিফান্ডের সময়)
        Task UpdateBalanceAsync(decimal amount, bool isCredit);

        // ৩. নির্দিষ্ট সময়ের মধ্যে কত টাকা রিফান্ড হয়েছে তার সামারি
        Task<decimal> GetTotalRefundedAmountAsync();

        // ৪. শেষ ট্রানজেকশন ডেট আপডেট করা
        Task UpdateLastTransactionDateAsync(DateTime transactionDate);
    }
}
