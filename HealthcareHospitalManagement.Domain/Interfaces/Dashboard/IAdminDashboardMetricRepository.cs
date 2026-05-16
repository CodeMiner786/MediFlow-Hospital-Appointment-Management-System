using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Dashboard
{
    public interface IAdminDashboardMetricRepository : IGenericRepository<AdminDashboardMetric>
    {
        // নির্দিষ্ট একটি তারিখের ড্যাশবোর্ড মেট্রিক খুঁজে বের করা
        Task<AdminDashboardMetric?> GetByDateAsync(DateOnly date);

        // আজকের লেটেস্ট মেট্রিক ডেটা পাওয়ার জন্য মেথড
        Task<AdminDashboardMetric?> GetTodayMetricsAsync();

        // একটি নির্দিষ্ট তারিখের রেঞ্জ অনুযায়ী ড্যাশবোর্ড ডেটা স্ট্রীম করা (যেমন: লাস্ট ৭ দিন)
        IAsyncEnumerable<AdminDashboardMetric> GetMetricsByDateRangeStream(DateOnly startDate, DateOnly endDate);
    }
}
