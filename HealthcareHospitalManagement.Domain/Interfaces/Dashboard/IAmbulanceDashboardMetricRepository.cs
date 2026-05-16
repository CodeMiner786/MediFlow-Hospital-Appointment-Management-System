using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Dashboard
{
    public interface IAmbulanceDashboardMetricRepository : IGenericRepository<AmbulanceDashboardMetric>
    {
        // নির্দিষ্ট একজন প্রোভাইডারের জন্য কোনো নির্দিষ্ট তারিখের মেট্রিক খুঁজে বের করা
        Task<AmbulanceDashboardMetric?> GetByProviderAndDateAsync(Guid providerId, DateOnly date);

        // একজন প্রোভাইডারের আজকের লেটেস্ট মেট্রিক ডেটা পাওয়ার মেথড
        Task<AmbulanceDashboardMetric?> GetTodayProviderMetricsAsync(Guid providerId, CancellationToken cancellationToken);

        // প্রোভাইডারের আয় এবং বুকিং হিস্ট্রি গ্রাফের জন্য ডেটা স্ট্রীম করা
        IAsyncEnumerable<AmbulanceDashboardMetric> GetProviderMetricsRangeStream(Guid providerId, DateOnly startDate, DateOnly endDate);
    }
}
