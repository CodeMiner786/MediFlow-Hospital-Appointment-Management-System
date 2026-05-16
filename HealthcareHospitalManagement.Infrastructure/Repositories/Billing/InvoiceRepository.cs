using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;
using HealthcareHospitalManagement.Domain.Interfaces.Billing;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Billing
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে পাস করা হয়েছে
    public class InvoiceRepository(ApplicationDbContext context)
        : GenericRepository<Invoice>(context), IInvoiceRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<Invoice> _dbSet = context.Set<Invoice>();

        // ইউনিক ইনভয়েস নাম্বার এবং সফট ডিলিট চেক করে ডাটা আনা হচ্ছে
        public async Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber && !i.IsDeleted);
        }

        // পেশেন্টের আইডির ওপর ভিত্তি করে তার সব ইনভয়েসের লিস্ট স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<Invoice> GetInvoicesByPatientIdStream(Guid patientId)
        {
            return _dbSet
                .Where(i => i.PatientId == patientId && !i.IsDeleted)
                .OrderByDescending(i => i.IssuedDate) // নতুন ইনভয়েস আগে থাকবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // স্ট্যাটাস ফিল্টার করে ইনভয়েসগুলো স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<Invoice> GetInvoicesByStatusStream(InvoiceStatus status)
        {
            return _dbSet
                .Where(i => i.Status == status && !i.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // বর্তমান সময়ের চেয়ে ডিউ ডেট কম এবং ইনভয়েসটি এখনো পেইড হয়নি এমন ডাটা ফিল্টার করা
        public IAsyncEnumerable<Invoice> GetOverdueInvoicesStream(DateTime currentDate)
        {
            return _dbSet
                .Where(i => i.DueDate < currentDate
                         && i.Status != InvoiceStatus.Paid
                         && !i.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
