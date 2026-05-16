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
    public class InsuranceClaimRepository(ApplicationDbContext context)
        : GenericRepository<InsuranceClaim>(context), IInsuranceClaimRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<InsuranceClaim> _dbSet = context.Set<InsuranceClaim>();

        // ক্লেইম নাম্বার এবং সফট ডিলিট চেক করে ডাটা আনা হচ্ছে
        public async Task<InsuranceClaim?> GetByClaimNumberAsync(string claimNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.ClaimNumber == claimNumber && !c.IsDeleted);
        }

        // বিল আইডির ওপর ভিত্তি করে ওয়ান-টু-ওয়ান রিলেশনের ক্লেইমটি খুঁজে বের করা হচ্ছে
        public async Task<InsuranceClaim?> GetByBillIdAsync(Guid billId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.BillId == billId && !c.IsDeleted);
        }

        // স্ট্যাটাস ফিল্টার করে ক্লেইমগুলো স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<InsuranceClaim> GetClaimsByStatusStream(InsuranceClaimStatus status)
        {
            return _dbSet
                .Where(c => c.Status == status && !c.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // প্রোভাইডারের নাম অনুযায়ী ক্লেইমগুলো ফিল্টার করে স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<InsuranceClaim> GetClaimsByProviderStream(string providerName)
        {
            return _dbSet
                .Where(c => c.InsuranceProvider == providerName && !c.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
