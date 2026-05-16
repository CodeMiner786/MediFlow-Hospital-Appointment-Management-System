using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    public class HospitalSettingsRepository(ApplicationDbContext context)
        : GenericRepository<HospitalSettings>(context), IHospitalSettingsRepository
    {
        private readonly DbSet<HospitalSettings> _dbSet = context.Set<HospitalSettings>();

        // 🔹 সাধারণত এই টেবিলে একটিই রো থাকে, তাই লেটেস্ট বা প্রথমটি নেওয়া হচ্ছে
        public async Task<HospitalSettings?> GetCurrentSettingsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.CreatedAt)
                .FirstOrDefaultAsync(ct);
        }

        // 🔹 অ্যাডমিন প্যানেল থেকে গ্লোবাল কমিশন রেট পরিবর্তন করার জন্য
        public async Task UpdateDefaultSharesAsync(decimal platformShare, decimal doctorShare, CancellationToken ct = default)
        {
            var settings = await GetCurrentSettingsAsync(ct);

            if (settings != null)
            {
                settings.DefaultPlatformSharePercent = platformShare;
                settings.DefaultDoctorSharePercent = doctorShare;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(settings, ct);
                await context.SaveChangesAsync(ct);
            }
            else
            {
                await AddAsync(new HospitalSettings
                {
                    DefaultPlatformSharePercent = platformShare,
                    DefaultDoctorSharePercent = doctorShare
                }, ct);

                await context.SaveChangesAsync(ct);
            }
        }
    }
}
