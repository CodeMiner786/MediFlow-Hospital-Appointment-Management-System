using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using HealthcareHospitalManagement.Domain.Interfaces.Dashboard;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Dashboard
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে পাস করা হয়েছে
    public class LabDashboardMetricRepository(ApplicationDbContext context)
        : GenericRepository<LabDashboardMetric>(context), ILabDashboardMetricRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<LabDashboardMetric> _dbSet = context.Set<LabDashboardMetric>();

        // ল্যাব আইডি এবং নির্দিষ্ট তারিখ মিল আছে কিনা এবং ডাটা ডিলিট করা হয়নি কিনা চেক করা হচ্ছে
        public async Task<LabDashboardMetric?> GetByLabAndDateAsync(Guid labProfileId, DateOnly date)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.LabProfileId == labProfileId && m.MetricDate == date && !m.IsDeleted);
        }

        // আজকের কারেন্ট ডেট অনুযায়ী ল্যাবের মেট্রিক রিটার্ন করা হচ্ছে
        public async Task<LabDashboardMetric?> GetTodayLabMetricsAsync(Guid labProfileId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await GetByLabAndDateAsync(labProfileId, today);
        }

        // এনালাইটিক্স বা রিপোর্টের জন্য ল্যাবের মেট্রিক ডেটাগুলো স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<LabDashboardMetric> GetLabMetricsRangeStream(Guid labProfileId, DateOnly startDate, DateOnly endDate)
        {
            return _dbSet
                // ল্যাব আইডি এবং তারিখের সীমানা অনুযায়ী ফিল্টার করা হচ্ছে
                .Where(m => m.LabProfileId == labProfileId
                         && m.MetricDate >= startDate
                         && m.MetricDate <= endDate
                         && !m.IsDeleted)
                // মেট্রিক ডাটাগুলো শুধুমাত্র রিড করার জন্য ব্যবহার হয়, তাই ট্র্যাকিং অফ রাখা হয়েছে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
