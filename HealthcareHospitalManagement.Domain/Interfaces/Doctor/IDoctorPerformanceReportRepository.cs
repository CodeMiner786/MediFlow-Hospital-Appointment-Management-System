using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorPerformanceReportRepository : IGenericRepository<DoctorPerformanceReport>
    {
        // নির্দিষ্ট একজন ডাক্তারের সব পারফরম্যান্স রিপোর্ট স্ট্রীম করা
        IAsyncEnumerable<DoctorPerformanceReport> GetReportsByDoctorStream(Guid doctorId);

        // নির্দিষ্ট পিরিয়ড টাইপ (যেমন: Monthly, Yearly) অনুযায়ী রিপোর্ট ফিল্টার করা
        IAsyncEnumerable<DoctorPerformanceReport> GetReportsByPeriodStream(Guid doctorId, ReportPeriodType periodType);

        // তারিখের রেঞ্জ অনুযায়ী রিপোর্ট খুঁজে বের করা (অডিট বা জেনারেশনের জন্য)
        Task<DoctorPerformanceReport?> GetReportByDateRangeAsync(Guid doctorId, DateTime fromDate, DateTime toDate);

        // নির্দিষ্ট ডিপার্টমেন্টের সব ডাক্তারের পারফরম্যান্স রিপোর্ট দেখা (এডমিনের জন্য)
        IAsyncEnumerable<DoctorPerformanceReport> GetReportsByDepartmentStream(string departmentName);

        // সর্বশেষ জেনারেট হওয়া রিপোর্টটি খুঁজে বের করা
        Task<DoctorPerformanceReport?> GetLatestReportAsync(Guid doctorId);
    }
}
