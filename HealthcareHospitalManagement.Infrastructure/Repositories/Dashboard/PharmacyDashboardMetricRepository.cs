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
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে ইনজেক্ট করা হয়েছে
    public class PharmacyDashboardMetricRepository(ApplicationDbContext context)
        : GenericRepository<PharmacyDashboardMetric>(context), IPharmacyDashboardMetricRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<PharmacyDashboardMetric> _dbSet = context.Set<PharmacyDashboardMetric>();

        // ফার্মেসি আইডি এবং নির্দিষ্ট তারিখ চেক করে মেট্রিক রেকর্ডটি নিয়ে আসা হচ্ছে
        public async Task<PharmacyDashboardMetric?> GetByPharmacyAndDateAsync(Guid pharmacyId, DateOnly date)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.PharmacyProfileId == pharmacyId && m.MetricDate == date && !m.IsDeleted);
        }

        // আজকের কারেন্ট ডেট ব্যবহার করে ফার্মেসির লেটেস্ট মেট্রিক রিটার্ন করা হচ্ছে
        public async Task<PharmacyDashboardMetric?> GetTodayPharmacyMetricsAsync(Guid pharmacyId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await GetByPharmacyAndDateAsync(pharmacyId, today);
        }

        // রিপোর্ট বা এনালাইটিক্স গ্রাফের জন্য ফার্মেসির মেট্রিক ডেটাগুলো স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<PharmacyDashboardMetric> GetPharmacyMetricsRangeStream(Guid pharmacyId, DateOnly startDate, DateOnly endDate)
        {
            return _dbSet
                // ফার্মেসি আইডি এবং তারিখের সীমা অনুযায়ী ফিল্টার করা হচ্ছে
                .Where(m => m.PharmacyProfileId == pharmacyId
                         && m.MetricDate >= startDate
                         && m.MetricDate <= endDate
                         && !m.IsDeleted)
                // পারফরম্যান্স অপ্টিমাইজেশনের জন্য ট্র্যাকিং অফ রাখা হয়েছে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
