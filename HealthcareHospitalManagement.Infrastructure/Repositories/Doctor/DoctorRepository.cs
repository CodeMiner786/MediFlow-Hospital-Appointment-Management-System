using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext ইনজেক্ট করা হয়েছে
    public class DoctorRepository(ApplicationDbContext context)
        : GenericRepository<DoctorEntity>(context), IDoctorRepository
    {
        private readonly DbSet<DoctorEntity> _dbSet = context.Set<DoctorEntity>();

        // ইউনিক ডাক্তার কোড দিয়ে ডাটা সার্চ করা
        public async Task<DoctorEntity?> GetByDoctorCodeAsync(string doctorCode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.DoctorCode == doctorCode && !d.IsDeleted);
        }

        // স্পেশালাইজেশন অনুযায়ী একটিভ ডাক্তারদের লিস্ট স্ট্রীম করা
        public IAsyncEnumerable<DoctorEntity> GetDoctorsBySpecializationStream(DoctorSpecialization specialization)
        {
            return _dbSet
                .Where(d => d.Specialization == specialization && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডিপার্টমেন্ট আইডি অনুযায়ী ডাক্তারদের ফিল্টার করা
        public IAsyncEnumerable<DoctorEntity> GetDoctorsByDepartmentStream(Guid departmentId)
        {
            return _dbSet
                .Where(d => d.DepartmentId == departmentId && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডাক্তারের প্রোফাইলের সাথে ডিপার্টমেন্ট এবং ফিডব্যাক সামারি একসাথে লোড করা
        public async Task<DoctorEntity?> GetDoctorFullProfileAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(d => d.Department)
                .Include(d => d.FeedbackSummary)
                .FirstOrDefaultAsync(d => d.Id == doctorId && !d.IsDeleted);
        }

        // লাইসেন্সের মেয়াদ চেক করার জন্য কুয়েরি
        public IAsyncEnumerable<DoctorEntity> GetDoctorsWithExpiringLicenseStream(DateTime thresholdDate)
        {
            return _dbSet
                .Where(d => d.LicenseExpiryDate <= thresholdDate && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // এভারেজ রেটিং অনুযায়ী সাজিয়ে সেরা কয়েকজন ডাক্তারকে রিটার্ন করা
        public async Task<IEnumerable<DoctorEntity>> GetTopRatedDoctorsAsync(int count)
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .OrderByDescending(d => d.AverageRating)
                .Take(count)
                .ToListAsync();
        }

        // ডাক্তারদের availability অনুযায়ী ফিল্টার করা
        public IAsyncEnumerable<DoctorEntity> GetAvailableDoctorsStream()
        {
            return _dbSet
                .Where(d => d.IsAvailableNow && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

    }
}
