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
    public class DoctorUnavailabilityRepository(ApplicationDbContext context)
        : GenericRepository<DoctorUnavailability>(context), IDoctorUnavailabilityRepository
    {
        private readonly DbSet<DoctorUnavailability> _dbSet = context.Set<DoctorUnavailability>();

        // ডাক্তারের আইডি অনুযায়ী সব রেকর্ড স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorUnavailability> GetUnavailabilitiesByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(u => u.DoctorId == doctorId && !u.IsDeleted)
                .OrderByDescending(u => u.UnavailableDate)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // নির্দিষ্ট দিনের অনুপস্থিতি ফিল্টার করা
        public IAsyncEnumerable<DoctorUnavailability> GetUnavailabilitiesByDateStream(Guid doctorId, DateTime date)
        {
            return _dbSet
                .Where(u => u.DoctorId == doctorId
                         && u.UnavailableDate.Date == date.Date
                         && !u.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // বর্তমান সময় অনুযায়ী ডাক্তার অনুপস্থিত কি না তা চেক করা
        public async Task<bool> IsDoctorUnavailableNowAsync(Guid doctorId)
        {
            var now = DateTime.UtcNow;
            var currentTime = TimeOnly.FromDateTime(now);

            return await _dbSet.AnyAsync(u =>
                u.DoctorId == doctorId &&
                u.UnavailableDate.Date == now.Date &&
                (u.IsFullDay || (u.FromTime <= currentTime && u.ToTime >= currentTime)) &&
                !u.IsDeleted);
        }

        // স্লট জেনারেট বা বুকিংয়ের সময় টাইমিং কনফ্লিক্ট চেক করা
        public async Task<bool> CheckConflictAsync(Guid doctorId, DateTime date, TimeOnly? fromTime, TimeOnly? toTime)
        {
            var query = _dbSet.Where(u => u.DoctorId == doctorId && u.UnavailableDate.Date == date.Date && !u.IsDeleted);

            if (fromTime.HasValue && toTime.HasValue)
            {
                // যদি নির্দিষ্ট সময় থাকে, তবে ফুল ডে অথবা সময়ের ওভারল্যাপ চেক হবে
                return await query.AnyAsync(u => u.IsFullDay || (u.FromTime < toTime && u.ToTime > fromTime));
            }

            // শুধু তারিখ থাকলে ওই দিনের যেকোনো রেকর্ড আছে কি না দেখা হবে
            return await query.AnyAsync();
        }
    }
}
