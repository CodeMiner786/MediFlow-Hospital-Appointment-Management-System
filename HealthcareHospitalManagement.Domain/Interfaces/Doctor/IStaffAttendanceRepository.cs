using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IStaffAttendanceRepository : IGenericRepository<StaffAttendance>
    {
        // নির্দিষ্ট একজন স্টাফের অ্যাটেনডেন্স হিস্ট্রি স্ট্রীম করা
        IAsyncEnumerable<StaffAttendance> GetAttendanceByStaffStream(Guid staffId);

        // নির্দিষ্ট একটি তারিখের সব স্টাফের উপস্থিতি দেখা (ডেইলি রিপোর্ট)
        IAsyncEnumerable<StaffAttendance> GetDailyAttendanceStream(DateOnly date);

        // নির্দিষ্ট মাসের উপস্থিতির রেকর্ড ফিল্টার করা
        IAsyncEnumerable<StaffAttendance> GetMonthlyAttendanceStream(Guid staffId, int month, int year);

        // স্টাফের চেক-ইন আপডেট করা
        Task UpdateCheckInAsync(Guid staffId, DateOnly date, TimeOnly checkInTime, CancellationToken ct = default);

        // স্টাফের চেক-আউট আপডেট করা
        Task UpdateCheckOutAsync(Guid staffId, DateOnly date, TimeOnly checkOutTime, CancellationToken ct = default);

        // আজ স্টাফ উপস্থিত আছে কি না তা যাচাই করা
        Task<StaffAttendance?> GetTodayAttendanceAsync(Guid staffId, CancellationToken ct = default);
    }
}
