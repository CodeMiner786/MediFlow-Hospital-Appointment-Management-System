using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorNotificationRepository : IGenericRepository<DoctorNotification>
    {
        // নির্দিষ্ট একজন ডাক্তারের সব নোটিফিকেশন স্ট্রীম করা
        IAsyncEnumerable<DoctorNotification> GetNotificationsByDoctorStream(Guid doctorId);

        // শুধুমাত্র আন-রিড (IsRead = false) নোটিফিকেশনগুলো খুঁজে বের করা
        IAsyncEnumerable<DoctorNotification> GetUnreadNotificationsByDoctorStream(Guid doctorId);

        // অতি জরুরি (IsUrgent = true) নোটিফিকেশনগুলো আলাদাভাবে পাওয়া
        IAsyncEnumerable<DoctorNotification> GetUrgentNotificationsStream(Guid doctorId);

        // সব নোটিফিকেশনকে একসাথে 'Read' হিসেবে মার্ক করা
        Task MarkAllAsReadAsync(Guid doctorId, CancellationToken ct = default);

        // নির্দিষ্ট একটি নোটিফিকেশনকে 'Read' হিসেবে মার্ক করা
        Task MarkAsReadAsync(Guid notificationId, CancellationToken ct = default);

        // নির্দিষ্ট ক্যাটাগরি (যেমন: Appointment) অনুযায়ী নোটিফিকেশন ফিল্টার করা
        IAsyncEnumerable<DoctorNotification> GetByReferenceIdAsync(Guid referenceId);
    }
}
