using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorLeaveRepository : IGenericRepository<DoctorLeave>
    {
        // নির্দিষ্ট একজন ডাক্তারের সব ছুটির রেকর্ড স্ট্রীম করা
        IAsyncEnumerable<DoctorLeave> GetLeavesByDoctorIdStream(Guid doctorId);

        // পেন্ডিং ছুটির আবেদনগুলো খুঁজে বের করা (অ্যাডমিন প্যানেলের জন্য)
        IAsyncEnumerable<DoctorLeave> GetPendingLeavesStream();

        // নির্দিষ্ট একটি তারিখের রেঞ্জে কারা ছুটিতে আছেন তা দেখা
        IAsyncEnumerable<DoctorLeave> GetLeavesByDateRangeStream(DateTime start, DateTime end);

        // ডাক্তার বর্তমানে ছুটিতে আছেন কিনা তা চেক করা
        Task<bool> IsDoctorOnLeaveAsync(Guid doctorId, DateTime date, CancellationToken ct = default);

        // ছুটির আবেদন অ্যাপ্রুভ বা রিজেক্ট করার মেথড
        Task UpdateLeaveStatusAsync(Guid leaveId, bool isApproved, string processorName, string? rejectionReason = null, CancellationToken ct = default);
    }
}
