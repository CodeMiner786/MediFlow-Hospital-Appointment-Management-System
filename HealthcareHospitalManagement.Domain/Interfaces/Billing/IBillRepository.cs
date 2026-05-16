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
    public interface IBillRepository : IGenericRepository<Bill>
    {
        // বিল নাম্বার দিয়ে সুনির্দিষ্ট বিল খুঁজে বের করা
        Task<Bill?> GetByBillNumberAsync(string billNumber);

        // নির্দিষ্ট একজন পেশেন্টের সব বিলের লিস্ট স্ট্রীম করা
        IAsyncEnumerable<Bill> GetBillsByPatientIdStream(Guid patientId);

        // পেমেন্ট স্ট্যাটাস (যেমন: Pending, Paid, PartiallyPaid) অনুযায়ী বিল ফিল্টার করা
        IAsyncEnumerable<Bill> GetBillsByStatusStream(PaymentStatus status);

        // বকেয়া (Due Amount) আছে এমন সব বিল খুঁজে বের করা
        IAsyncEnumerable<Bill> GetOverdueBillsStream();
    }
}
