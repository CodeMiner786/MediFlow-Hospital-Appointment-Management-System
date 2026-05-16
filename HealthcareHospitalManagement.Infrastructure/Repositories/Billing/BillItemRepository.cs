using HealthcareHospitalManagement.Domain.Entities.Billing;
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
    public class BillItemRepository(ApplicationDbContext context)
        : GenericRepository<BillItem>(context), IBillItemRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<BillItem> _dbSet = context.Set<BillItem>();

        // একটি নির্দিষ্ট বিলের সাথে যুক্ত সব আইটেম স্ট্রীম আকারে রিটার্ন করা হচ্ছে
        public IAsyncEnumerable<BillItem> GetItemsByBillIdStream(Guid billId)
        {
            return _dbSet
                // বিল আইডি এবং সফট ডিলিট চেক করা হচ্ছে
                .Where(bi => bi.BillId == billId && !bi.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // সার্ভিস কোড ব্যবহার করে নির্দিষ্ট সার্ভিস আইটেমগুলো ডাটাবেজ থেকে আনা হচ্ছে
        public async Task<IEnumerable<BillItem>> GetItemsByServiceCodeAsync(string serviceCode)
        {
            return await _dbSet
                .Where(bi => bi.ServiceCode == serviceCode && !bi.IsDeleted)
                .ToListAsync();
        }
    }
}
