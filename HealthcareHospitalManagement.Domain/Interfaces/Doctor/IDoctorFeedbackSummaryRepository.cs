using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorFeedbackSummaryRepository : IGenericRepository<DoctorFeedbackSummary>
    {
        // ডাক্তার আইডি দিয়ে সরাসরি তার ফিডব্যাক সামারি খুঁজে বের করা
        Task<DoctorFeedbackSummary?> GetByDoctorIdAsync(Guid doctorId, CancellationToken ct = default);

        // নির্দিষ্ট রেটিং এর চেয়ে বেশি (যেমন: ৪ স্টারের উপরে) এমন ডাক্তারদের সামারি স্ট্রীম করা
        IAsyncEnumerable<DoctorFeedbackSummary> GetHighRatedSummariesStream(decimal minRating);

        // নতুন রিভিউ আসার পর সামারি আপডেট করার জন্য মেথড
        Task UpdateSummaryStatsAsync(Guid doctorId, decimal newAverage, int totalReviews, CancellationToken ct = default);
    }
}
