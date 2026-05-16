using HealthcareHospitalManagement.Domain.Entities.Feedback;
using HealthcareHospitalManagement.Domain.Enums.FeedbackRating;
using HealthcareHospitalManagement.Domain.Interfaces.Feedback;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Feedback
{
    public class FeedbackRepository(ApplicationDbContext context)
        : GenericRepository<FeedbackEntity>(context), IFeedbackRepository
    {
        private readonly DbSet<FeedbackEntity> _dbSet = context.Set<FeedbackEntity>();

        // 🔹 ডক্টর অনুযায়ী ফিডব্যাক স্ট্রিম (Include Patient to show who gave feedback)
        public IAsyncEnumerable<FeedbackEntity> GetFeedbacksByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(f => f.DoctorId == doctorId && f.IsPublished && !f.IsDeleted)
                .Include(f => f.Patient)
                .OrderByDescending(f => f.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // 🔹 পেশেন্ট নিজের কোন কোন ফিডব্যাক দিয়েছে তা দেখা
        public IAsyncEnumerable<FeedbackEntity> GetFeedbacksByPatientStream(Guid patientId)
        {
            return _dbSet
                .Where(f => f.PatientId == patientId && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .AsAsyncEnumerable();
        }

        // 🔹 অ্যাডমিন এখনো যে ফিডব্যাকগুলো চেক করেনি
        public async Task<IEnumerable<FeedbackEntity>> GetPendingFeedbacksAsync()
        {
            return await _dbSet
                .Where(f => f.Status == FeedbackStatus.Pending && !f.IsDeleted)
                .ToListAsync();
        }

        // 🔹 নির্দিষ্ট ডক্টরের এভারেজ রেটিং (LINQ Average)
        public async Task<double> GetAverageRatingForDoctorAsync(Guid doctorId)
        {
            var ratings = _dbSet.Where(f => f.DoctorId == doctorId && !f.IsDeleted);
            if (!await ratings.AnyAsync()) return 0;

            return await ratings.AverageAsync(f => f.Rating);
        }

        // 🔹 ক্যাটাগরি অনুযায়ী (Doctor, Pharmacy, etc) পাবলিশড ফিডব্যাক
        public async Task<IEnumerable<FeedbackEntity>> GetPublishedFeedbacksByTypeAsync(FeedbackType type)
        {
            return await _dbSet
                .Where(f => f.FeedbackType == type && f.IsPublished && !f.IsDeleted)
                .ToListAsync();
        }
    }
}
