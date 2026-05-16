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
    public class DoctorDashboardMetricRepository(ApplicationDbContext context)
        : GenericRepository<DoctorDashboardMetric>(context), IDoctorDashboardMetricRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<DoctorDashboardMetric> _dbSet = context.Set<DoctorDashboardMetric>();

        // ডাক্তার আইডি এবং নির্দিষ্ট তারিখ দিয়ে মেট্রিক রেকর্ড খুঁজে বের করা হচ্ছে
        public async Task<DoctorDashboardMetric?> GetByDoctorAndDateAsync(Guid doctorId, DateOnly date)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.DoctorId == doctorId && m.MetricDate == date && !m.IsDeleted);
        }

        // আজকের দিনের কারেন্ট ডেটা পাওয়ার জন্য শর্টকাট মেথড
        public async Task<DoctorDashboardMetric?> GetTodayDoctorMetricsAsync(Guid doctorId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await GetByDoctorAndDateAsync(doctorId, today);
        }

        // নির্দিষ্ট সময়ের ব্যবধানে ডাক্তারের সব মেট্রিক ডেটা স্ট্রীম আকারে পাঠানো হচ্ছে (রিপোর্ট বা গ্রাফের জন্য)
        public IAsyncEnumerable<DoctorDashboardMetric> GetDoctorMetricsRangeStream(Guid doctorId, DateOnly startDate, DateOnly endDate)
        {
            return _dbSet
                // ডাক্তার আইডি এবং তারিখের সীমা অনুযায়ী ফিল্টার করা হচ্ছে
                .Where(m => m.DoctorId == doctorId
                         && m.MetricDate >= startDate
                         && m.MetricDate <= endDate
                         && !m.IsDeleted)
                // পারফরম্যান্সের জন্য ট্র্যাকিং অফ রাখা হয়েছে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
