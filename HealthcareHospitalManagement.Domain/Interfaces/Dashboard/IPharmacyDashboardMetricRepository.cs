using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Dashboard
{
    public interface IPharmacyDashboardMetricRepository : IGenericRepository<PharmacyDashboardMetric>
    {
        // নির্দিষ্ট ফার্মেসি প্রোফাইল এবং তারিখ অনুযায়ী মেট্রিক ডেটা খুঁজে বের করা
        Task<PharmacyDashboardMetric?> GetByPharmacyAndDateAsync(Guid pharmacyId, DateOnly date);

        // একটি নির্দিষ্ট ফার্মেসির আজকের দিনের কারেন্ট মেট্রিক পাওয়ার মেথড
        Task<PharmacyDashboardMetric?> GetTodayPharmacyMetricsAsync(Guid pharmacyId, CancellationToken cancellationToken);

        // ফার্মেসির সেলস এবং ইনভেন্টরি ট্রেন্ড গ্রাফের জন্য নির্দিষ্ট রেঞ্জের ডেটা স্ট্রীম করা
        IAsyncEnumerable<PharmacyDashboardMetric> GetPharmacyMetricsRangeStream(Guid pharmacyId, DateOnly startDate, DateOnly endDate);
    }
}
