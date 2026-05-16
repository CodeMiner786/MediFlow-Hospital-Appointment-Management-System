using HealthcareHospitalManagement.Domain.Entities.Doctor;
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
    // Primary Constructor ব্যবহার করে ApplicationDbContext ইনজেক্ট করা হয়েছে
    public class DoctorScheduleRepository(ApplicationDbContext context)
        : GenericRepository<DoctorSchedule>(context), IDoctorScheduleRepository
    {
        private readonly DbSet<DoctorSchedule> _dbSet = context.Set<DoctorSchedule>();

        // ডাক্তারের সব শিডিউল ডে-ওয়াইজ সাজিয়ে স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorSchedule> GetSchedulesByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(s => s.DoctorId == doctorId && !s.IsDeleted)
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // নির্দিষ্ট বারের শিডিউল ফিল্টার করা (যেমন: শুধু রবিবারের ডিউটি)
        public IAsyncEnumerable<DoctorSchedule> GetSchedulesByDayStream(Guid doctorId, DayOfWeek day)
        {
            return _dbSet
                .Where(s => s.DoctorId == doctorId && s.DayOfWeek == day && !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ইফেক্টিভ ডেট রেঞ্জ চেক করে বর্তমানে কার্যকর শিডিউলগুলো আনা
        public IAsyncEnumerable<DoctorSchedule> GetActiveSchedulesStream(Guid doctorId)
        {
            var now = DateTime.UtcNow;
            return _dbSet
                .Where(s => s.DoctorId == doctorId
                         && s.IsAvailable
                         && s.EffectiveFrom <= now
                         && (s.EffectiveTo == null || s.EffectiveTo >= now)
                         && !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // স্লট জেনারেশন বা ডিটেইলস ভিউর জন্য স্লটসহ ডাটা লোড করা
        public async Task<DoctorSchedule?> GetScheduleWithSlotsAsync(Guid scheduleId)
        {
            return await _dbSet
                .Include(s => s.Slots)
                .FirstOrDefaultAsync(s => s.Id == scheduleId && !s.IsDeleted);
        }

        // নতুন শিডিউল তৈরির সময় টাইমিং ওভারল্যাপ হচ্ছে কিনা তা চেক করা (Validation)
        public async Task<bool> HasScheduleOverlapAsync(Guid doctorId, DayOfWeek day, TimeOnly start, TimeOnly end)
        {
            return await _dbSet.AnyAsync(s =>
                s.DoctorId == doctorId &&
                s.DayOfWeek == day &&
                s.StartTime < end &&
                s.EndTime > start &&
                !s.IsDeleted);
        }
    }
}
