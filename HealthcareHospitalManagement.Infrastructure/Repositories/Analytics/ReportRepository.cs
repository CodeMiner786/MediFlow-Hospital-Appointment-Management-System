using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Enums.Analytics;
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
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে পাস করা হয়েছে
    public class ReportRepository(ApplicationDbContext context)
        : GenericRepository<ReportEntity>(context), IReportRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<ReportEntity> _dbSet = context.Set<ReportEntity>();

        // রিপোর্ট টাইপ ফিল্টার করে ডেটাগুলো স্ট্রীম আকারে রিটার্ন করা হচ্ছে
        public IAsyncEnumerable<ReportEntity> GetReportsByTypeStream(ReportType type)
        {
            return _dbSet
                // টাইপ চেক এবং সফট ডিলিট চেক করা হচ্ছে
                .Where(r => r.ReportType == type && !r.IsDeleted)
                // পারফরম্যান্সের জন্য ট্র্যাকিং বন্ধ রাখা হয়েছে
                .AsNoTracking()
                // ডাটাবেজ থেকে স্ট্রীম হিসেবে ডেটা পাঠানো হচ্ছে
                .AsAsyncEnumerable();
        }

        // জেনারেট হওয়ার তারিখ অনুযায়ী রিপোর্টের লিস্ট স্ট্রীম করা
        public IAsyncEnumerable<ReportEntity> GetReportsByGeneratedDateRangeStream(DateTime start, DateTime end)
        {
            return _dbSet
                // নির্দিষ্ট সময়সীমার মধ্যে জেনারেট হওয়া রিপোর্ট ফিল্টার করা হচ্ছে
                .Where(r => r.GeneratedAt >= start && r.GeneratedAt <= end && !r.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // নির্দিষ্ট ক্যাটাগরির সবচেয়ে নতুন (Latest) রিপোর্টটি খুঁজে বের করা
        public async Task<ReportEntity?> GetLatestReportByCategoryAsync(ReportCategory category, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(r => r.Category == category && !r.IsDeleted)
                // জেনারেট হওয়ার সময় অনুযায়ী উল্টোভাবে সাজিয়ে প্রথমটি নেওয়া হচ্ছে
                .OrderByDescending(r => r.GeneratedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
