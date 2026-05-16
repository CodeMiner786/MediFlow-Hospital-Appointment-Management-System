using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Interfaces.Analytics;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Analytics
{
    // Primary Constructor-এ DbContext এর বদলে ApplicationDbContext ব্যবহার করা হয়েছে
    public class DashboardMetricRepository(ApplicationDbContext context)
        : GenericRepository<DashboardMetric>(context), IDashboardMetricRepository
    {
        // ডেটাবেজ টেবিল (DbSet) কে সরাসরি কুয়েরি করার জন্য একটি ভেরিয়েবলে রাখা হয়েছে
        private readonly DbSet<DashboardMetric> _dbSet = context.Set<DashboardMetric>();

        // MetricDate প্রপার্টি ব্যবহার করে নির্দিষ্ট একটি দিনের মেট্রিক খুঁজে বের করা
        public async Task<DashboardMetric?> GetByDateAsync(DateOnly date)
        {
            return await _dbSet
                // তারিখ মিল আছে কিনা এবং ডেটাটি সফট ডিলিট করা হয়নি কিনা তা চেক করা হচ্ছে
                .FirstOrDefaultAsync(m => m.MetricDate == date && !m.IsDeleted);
        }

        // তারিখের রেঞ্জ অনুযায়ী ডেটা ফিল্টার করে IAsyncEnumerable (স্ট্রীমিং) আকারে রিটার্ন করা
        public IAsyncEnumerable<DashboardMetric> GetMetricsByDateRangeStream(DateOnly startDate, DateOnly endDate)
        {
            return _dbSet
                // শুরু এবং শেষ তারিখের মধ্যবর্তী ডেটাগুলো ফিল্টার করা হচ্ছে
                .Where(m => m.MetricDate >= startDate && m.MetricDate <= endDate && !m.IsDeleted)
                // মেট্রিক ডেটাগুলো সাধারণত রিড-অনলি হয়, তাই ট্র্যাকিং অফ রাখা হয়েছে পারফরম্যান্সের জন্য
                .AsNoTracking()
                // ডাটাবেজ থেকে ডেটাগুলো চাঙ্ক আকারে স্ট্রীম হিসেবে পাঠানো হচ্ছে
                .AsAsyncEnumerable();
        }
    }
}
