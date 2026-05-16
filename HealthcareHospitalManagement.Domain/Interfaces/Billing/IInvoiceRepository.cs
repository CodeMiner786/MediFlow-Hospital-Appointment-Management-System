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
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        // ইনভয়েস নাম্বার দিয়ে সুনির্দিষ্ট ইনভয়েস খুঁজে বের করা
        Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber);

        // নির্দিষ্ট একজন পেশেন্টের সব ইনভয়েস স্ট্রীম আকারে পাওয়া
        IAsyncEnumerable<Invoice> GetInvoicesByPatientIdStream(Guid patientId);

        // ইনভয়েস স্ট্যাটাস (যেমন: Draft, Issued, Paid, Cancelled) অনুযায়ী ফিল্টার করা
        IAsyncEnumerable<Invoice> GetInvoicesByStatusStream(InvoiceStatus status);

        // ডিউ ডেট (DueDate) পার হয়ে গেছে এমন ইনভয়েসগুলো খুঁজে বের করা
        IAsyncEnumerable<Invoice> GetOverdueInvoicesStream(DateTime currentDate);
    }
}
