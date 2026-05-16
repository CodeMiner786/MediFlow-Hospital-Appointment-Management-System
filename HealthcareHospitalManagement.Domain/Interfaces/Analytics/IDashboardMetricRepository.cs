using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Analytics
{
    public interface IDashboardMetricRepository : IGenericRepository<DashboardMetric>
    {
        // নির্দিষ্ট একটি তারিখের মেট্রিক ডেটা খুঁজে বের করার মেথড
        Task<DashboardMetric?> GetByDateAsync(DateOnly date);

        // একটি নির্দিষ্ট তারিখের রেঞ্জ (শুরু থেকে শেষ) অনুযায়ী ডেটা স্ট্রীম করার মেথড
        IAsyncEnumerable<DashboardMetric> GetMetricsByDateRangeStream(DateOnly startDate, DateOnly endDate);
    }
}
