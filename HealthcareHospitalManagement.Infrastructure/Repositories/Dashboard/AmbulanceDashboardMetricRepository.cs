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
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে পাঠানো হয়েছে
    public class AmbulanceDashboardMetricRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceDashboardMetric>(context), IAmbulanceDashboardMetricRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<AmbulanceDashboardMetric> _dbSet = context.Set<AmbulanceDashboardMetric>();

        // প্রোভাইডার আইডি এবং নির্দিষ্ট তারিখ দিয়ে মেট্রিক রেকর্ড খুঁজে বের করা হচ্ছে
        public async Task<AmbulanceDashboardMetric?> GetByProviderAndDateAsync(Guid providerId, DateOnly date)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.ProviderId == providerId && m.MetricDate == date && !m.IsDeleted);
        }

        // আজকের দিনের কারেন্ট ডেটা পাওয়ার জন্য শর্টকাট মেথড
        public async Task<AmbulanceDashboardMetric?> GetTodayProviderMetricsAsync(Guid providerId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await GetByProviderAndDateAsync(providerId, today);
        }

        // নির্দিষ্ট সময়ের ব্যবধানে প্রোভাইডারের সব মেট্রিক ডেটা স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<AmbulanceDashboardMetric> GetProviderMetricsRangeStream(Guid providerId, DateOnly startDate, DateOnly endDate)
        {
            return _dbSet
                // প্রোভাইডার আইডি এবং তারিখের সীমা অনুযায়ী ফিল্টার
                .Where(m => m.ProviderId == providerId
                         && m.MetricDate >= startDate
                         && m.MetricDate <= endDate
                         && !m.IsDeleted)
                // পারফরম্যান্সের জন্য ট্র্যাকিং অফ রাখা হয়েছে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
