using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Billing
{
    public interface IInsuranceClaimRepository : IGenericRepository<InsuranceClaim>
    {
        // ইন্স্যুরেন্সের ক্লেইম নাম্বার দিয়ে সুনির্দিষ্ট ক্লেইম খুঁজে বের করা
        Task<InsuranceClaim?> GetByClaimNumberAsync(string claimNumber);

        // নির্দিষ্ট কোনো বিল আইডির (BillId) সাথে যুক্ত ক্লেইম খুঁজে বের করা
        Task<InsuranceClaim?> GetByBillIdAsync(Guid billId);

        // ক্লেইম স্ট্যাটাস (যেমন: Submitted, Approved, Rejected) অনুযায়ী স্ট্রীম করা
        IAsyncEnumerable<InsuranceClaim> GetClaimsByStatusStream(InsuranceClaimStatus status);

        // নির্দিষ্ট ইন্স্যুরেন্স প্রোভাইডার অনুযায়ী সব ক্লেইম স্ট্রীম করা
        IAsyncEnumerable<InsuranceClaim> GetClaimsByProviderStream(string providerName);
    }
}
