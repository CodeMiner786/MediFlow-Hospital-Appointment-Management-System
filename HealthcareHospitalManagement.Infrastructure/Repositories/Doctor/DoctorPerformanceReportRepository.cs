using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.Report;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext ইনজেক্ট করা হয়েছে
    public class DoctorPerformanceReportRepository(ApplicationDbContext context)
        : GenericRepository<DoctorPerformanceReport>(context), IDoctorPerformanceReportRepository
    {
        private readonly DbSet<DoctorPerformanceReport> _dbSet = context.Set<DoctorPerformanceReport>();

        // ডাক্তারের সব রিপোর্ট সময় অনুযায়ী সাজিয়ে স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorPerformanceReport> GetReportsByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(r => r.DoctorId == doctorId && !r.IsDeleted)
                .OrderByDescending(r => r.FromDate)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // মাসিক বা বাৎসরিক রিপোর্ট আলাদা করার জন্য ফিল্টার
        public IAsyncEnumerable<DoctorPerformanceReport> GetReportsByPeriodStream(Guid doctorId, ReportPeriodType periodType)
        {
            return _dbSet
                .Where(r => r.DoctorId == doctorId && r.PeriodType == periodType && !r.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডুপ্লিকেট রিপোর্ট জেনারেশন এড়াতে নির্দিষ্ট ডেট রেঞ্জ চেক করা
        public async Task<DoctorPerformanceReport?> GetReportByDateRangeAsync(Guid doctorId, DateTime fromDate, DateTime toDate)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.DoctorId == doctorId
                                     && r.FromDate == fromDate
                                     && r.ToDate == toDate
                                     && !r.IsDeleted);
        }

        // এডমিন প্যানেলের এনালাইটিক্সের জন্য ডিপার্টমেন্ট ভিত্তিক রিপোর্ট স্ট্রীম করা
        public IAsyncEnumerable<DoctorPerformanceReport> GetReportsByDepartmentStream(string departmentName)
        {
            return _dbSet
                .Where(r => r.Department == departmentName && !r.IsDeleted)
                .OrderByDescending(r => r.GeneratedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ড্যাশবোর্ডে দেখানোর জন্য লেটেস্ট জেনারেটেড রিপোর্টটি রিটার্ন করা
        public async Task<DoctorPerformanceReport?> GetLatestReportAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(r => r.DoctorId == doctorId && !r.IsDeleted)
                .OrderByDescending(r => r.GeneratedAt)
                .FirstOrDefaultAsync();
        }
    }
}
