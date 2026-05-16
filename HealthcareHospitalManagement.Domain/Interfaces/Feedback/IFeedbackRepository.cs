using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feedback;
using HealthcareHospitalManagement.Domain.Enums.FeedbackRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feedback
{
    public interface IFeedbackRepository : IGenericRepository<FeedbackEntity>
    {
        // ১. নির্দিষ্ট কোনো ডক্টরের সব ফিডব্যাক দেখা
        IAsyncEnumerable<FeedbackEntity> GetFeedbacksByDoctorStream(Guid doctorId);

        // ২. নির্দিষ্ট কোনো পেশেন্টের দেওয়া সব ফিডব্যাক
        IAsyncEnumerable<FeedbackEntity> GetFeedbacksByPatientStream(Guid patientId);

        // ৩. পেন্ডিং ফিডব্যাকগুলো দেখা (অ্যাডমিন প্যানেলের জন্য)
        Task<IEnumerable<FeedbackEntity>> GetPendingFeedbacksAsync();

        // ৪. গড় রেটিং বের করা (যেমন: ডক্টরের এভারেজ রেটিং কত)
        Task<double> GetAverageRatingForDoctorAsync(Guid doctorId);

        // ৫. পাবলিশড ফিডব্যাকগুলো ফিল্টার করা
        Task<IEnumerable<FeedbackEntity>> GetPublishedFeedbacksByTypeAsync(FeedbackType type);
    }
}
