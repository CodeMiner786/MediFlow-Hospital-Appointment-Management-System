using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে ইনজেক্ট করা হয়েছে
    public class DoctorAvailabilityLogRepository(ApplicationDbContext context)
        : GenericRepository<DoctorAvailabilityLog>(context), IDoctorAvailabilityLogRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<DoctorAvailabilityLog> _dbSet = context.Set<DoctorAvailabilityLog>();

        // ডাক্তারের আইডি অনুযায়ী সব পরিবর্তনের ইতিহাস স্ট্রীম আকারে রিটার্ন করা হচ্ছে
        public IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByDoctorIdStream(Guid doctorId)
        {
            return _dbSet
                .Where(l => l.DoctorId == doctorId && !l.IsDeleted)
                .OrderByDescending(l => l.ChangedAt) // সর্বশেষ পরিবর্তনগুলো আগে থাকবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // স্ট্যাটাস অনুযায়ী ফিল্টার করে লগগুলো পাঠানো হচ্ছে
        public IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByStatusStream(DoctorAvailabilityStatus status)
        {
            return _dbSet
                .Where(l => l.Status == status && !l.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // তারিখের রেঞ্জ অনুযায়ী অডিট লগগুলো স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByDateRangeStream(DateTime start, DateTime end)
        {
            return _dbSet
                .Where(l => l.ChangedAt >= start && l.ChangedAt <= end && !l.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডাক্তারের বর্তমান বা লেটেস্ট অ্যাভেইল্যাবিলিটি রেকর্ডটি খুঁজে বের করা হচ্ছে
        public async Task<DoctorAvailabilityLog?> GetLatestLogByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(l => l.DoctorId == doctorId && !l.IsDeleted)
                .OrderByDescending(l => l.ChangedAt)
                .FirstOrDefaultAsync();
        }
    }
}
