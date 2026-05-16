using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Enums.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Analytics
{
    public interface IReportRepository : IGenericRepository<ReportEntity>
    {
        // রিপোর্ট টাইপ (Financial, Medical ইত্যাদি) অনুযায়ী রিপোর্ট খুঁজে বের করা
        IAsyncEnumerable<ReportEntity> GetReportsByTypeStream(ReportType type);

        // নির্দিষ্ট সময়ের ব্যবধানে জেনারেট হওয়া রিপোর্টগুলো স্ট্রীম করা
        IAsyncEnumerable<ReportEntity> GetReportsByGeneratedDateRangeStream(DateTime start, DateTime end);

        // নির্দিষ্ট ক্যাটাগরির সর্বশেষ রিপোর্টটি খুঁজে বের করা
        Task<ReportEntity?> GetLatestReportByCategoryAsync(ReportCategory category, CancellationToken cancellationToken);
    }
}
