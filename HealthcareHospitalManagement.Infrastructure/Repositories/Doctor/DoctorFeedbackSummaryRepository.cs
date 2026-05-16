using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    public class DoctorFeedbackSummaryRepository(ApplicationDbContext context)
        : GenericRepository<DoctorFeedbackSummary>(context), IDoctorFeedbackSummaryRepository
    {
        private readonly DbSet<DoctorFeedbackSummary> _dbSet = context.Set<DoctorFeedbackSummary>();

        public async Task<DoctorFeedbackSummary?> GetByDoctorIdAsync(Guid doctorId, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId && !s.IsDeleted, ct);
        }

        public IAsyncEnumerable<DoctorFeedbackSummary> GetHighRatedSummariesStream(decimal minRating)
        {
            return _dbSet
                .Where(s => s.AverageRating >= minRating && !s.IsDeleted)
                .OrderByDescending(s => s.AverageRating)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task UpdateSummaryStatsAsync(Guid doctorId, decimal newAverage, int totalReviews, CancellationToken ct = default)
        {
            var summary = await GetByDoctorIdAsync(doctorId, ct);
            if (summary != null)
            {
                summary.AverageRating = newAverage;
                summary.TotalReviews = totalReviews;
                summary.LastUpdatedAt = DateTime.UtcNow;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(summary, ct);
            }
        }
    }
}
