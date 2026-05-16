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
    public class BillRepository(ApplicationDbContext context)
        : GenericRepository<Bill>(context), IBillRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<Bill> _dbSet = context.Set<Bill>();

        // ইউনিক বিল নাম্বার এবং সফট ডিলিট চেক করে বিল ডাটা আনা হচ্ছে
        public async Task<Bill?> GetByBillNumberAsync(string billNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(b => b.BillNumber == billNumber && !b.IsDeleted);
        }

        // পেশেন্টের আইডির ওপর ভিত্তি করে তার সব বিলের লিস্ট স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<Bill> GetBillsByPatientIdStream(Guid patientId)
        {
            return _dbSet
                .Where(b => b.PatientId == patientId && !b.IsDeleted)
                .OrderByDescending(b => b.BillDate) // নতুন বিল আগে থাকবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // পেমেন্ট স্ট্যাটাস অনুযায়ী ফিল্টার করে স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<Bill> GetBillsByStatusStream(PaymentStatus status)
        {
            return _dbSet
                .Where(b => b.PaymentStatus == status && !b.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // যে বিলগুলোর টোটাল অ্যামাউন্ট পেইড অ্যামাউন্টের চেয়ে বেশি (বকেয়া আছে) সেগুলো ফিল্টার করা হচ্ছে
        public IAsyncEnumerable<Bill> GetOverdueBillsStream()
        {
            return _dbSet
                .Where(b => b.TotalAmount > b.PaidAmount && !b.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
