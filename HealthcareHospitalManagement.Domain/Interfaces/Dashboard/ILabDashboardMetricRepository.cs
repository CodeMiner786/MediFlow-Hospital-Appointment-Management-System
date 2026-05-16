using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Dashboard
{
    public interface ILabDashboardMetricRepository : IGenericRepository<LabDashboardMetric>
    {
        // নির্দিষ্ট ল্যাব প্রোফাইল এবং তারিখ অনুযায়ী মেট্রিক খুঁজে বের করা
        Task<LabDashboardMetric?> GetByLabAndDateAsync(Guid labProfileId, DateOnly date);

        // একটি নির্দিষ্ট ল্যাবের আজকের দিনের মেট্রিক পাওয়ার মেথড
        Task<LabDashboardMetric?> GetTodayLabMetricsAsync(Guid labProfileId, CancellationToken cancellationToken);

        // ল্যাবের পারফরম্যান্স এবং রেভিনিউ গ্রাফের জন্য নির্দিষ্ট রেঞ্জের ডেটা স্ট্রীম করা
        IAsyncEnumerable<LabDashboardMetric> GetLabMetricsRangeStream(Guid labProfileId, DateOnly startDate, DateOnly endDate);
    }
}
