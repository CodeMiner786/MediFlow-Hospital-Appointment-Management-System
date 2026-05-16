using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Dashboard
{
    public interface IDoctorDashboardMetricRepository : IGenericRepository<DoctorDashboardMetric>
    {
        // নির্দিষ্ট একজন ডাক্তারের জন্য কোনো নির্দিষ্ট তারিখের মেট্রিক খুঁজে বের করা
        Task<DoctorDashboardMetric?> GetByDoctorAndDateAsync(Guid doctorId, DateOnly date);

        // একজন ডাক্তারের আজকের লেটেস্ট মেট্রিক ডেটা পাওয়ার মেথড
        Task<DoctorDashboardMetric?> GetTodayDoctorMetricsAsync(Guid doctorId, CancellationToken cancellationToken);

        // ডাক্তারের আয় এবং পেশেন্ট হিস্ট্রি গ্রাফের জন্য ডেটা স্ট্রীম করা
        IAsyncEnumerable<DoctorDashboardMetric> GetDoctorMetricsRangeStream(Guid doctorId, DateOnly startDate, DateOnly endDate);
    }
}
