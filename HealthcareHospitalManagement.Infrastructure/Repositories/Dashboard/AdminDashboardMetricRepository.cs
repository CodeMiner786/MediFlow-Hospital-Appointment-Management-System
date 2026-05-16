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
    public class AdminDashboardMetricRepository(ApplicationDbContext context)
        : GenericRepository<AdminDashboardMetric>(context), IAdminDashboardMetricRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<AdminDashboardMetric> _dbSet = context.Set<AdminDashboardMetric>();

        // তারিখ অনুযায়ী নির্দিষ্ট দিনের মেট্রিক রেকর্ড খুঁজে বের করা হচ্ছে
        public async Task<AdminDashboardMetric?> GetByDateAsync(DateOnly date)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.MetricDate == date && !m.IsDeleted);
        }

        // আজকের দিনের কারেন্ট মেট্রিক ডেটাটি রিটার্ন করা হচ্ছে
        public async Task<AdminDashboardMetric?> GetTodayMetricsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await GetByDateAsync(today);
        }

        // এনালাইটিক্স গ্রাফ দেখানোর জন্য নির্দিষ্ট রেঞ্জের ডেটা স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<AdminDashboardMetric> GetMetricsByDateRangeStream(DateOnly startDate, DateOnly endDate)
        {
            return _dbSet
                // শুরু এবং শেষ তারিখের মধ্যবর্তী ডেটা ফিল্টার করা হচ্ছে
                .Where(m => m.MetricDate >= startDate && m.MetricDate <= endDate && !m.IsDeleted)
                // মেট্রিক ডেটাগুলো সাধারণত চার্টে দেখানোর জন্য লাগে, তাই ট্র্যাকিং অফ রাখা হয়েছে
                .AsNoTracking()
                // ডেটাগুলো স্ট্রীম আকারে পাঠানো হচ্ছে
                .AsAsyncEnumerable();
        }
    }
}
