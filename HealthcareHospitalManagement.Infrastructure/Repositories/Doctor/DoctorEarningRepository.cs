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
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে ইনজেক্ট করা হয়েছে
    public class DoctorEarningRepository(ApplicationDbContext context)
        : GenericRepository<DoctorEarning>(context), IDoctorEarningRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<DoctorEarning> _dbSet = context.Set<DoctorEarning>();

        // ডাক্তারের আইডি এবং সফট ডিলিট চেক করে সব আয়ের লিস্ট স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorEarning> GetEarningsByDoctorIdStream(Guid doctorId)
        {
            return _dbSet
                .Where(e => e.DoctorId == doctorId && !e.IsDeleted)
                .OrderByDescending(e => e.EarningDate) // লেটেস্ট ইনকাম আগে থাকবে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // তারিখের রেঞ্জ অনুযায়ী ফিল্টার করে আয়ের ডাটা স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorEarning> GetEarningsByDateRangeStream(Guid doctorId, DateTime start, DateTime end)
        {
            return _dbSet
                .Where(e => e.DoctorId == doctorId
                         && e.EarningDate >= start
                         && e.EarningDate <= end
                         && !e.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // যে পেমেন্টগুলো ডাক্তার এখনো পাননি (Pending) সেগুলো ফিল্টার করা
        public IAsyncEnumerable<DoctorEarning> GetUnpaidEarningsStream(Guid doctorId)
        {
            return _dbSet
                .Where(e => e.DoctorId == doctorId && !e.IsPaid && !e.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // অ্যাপয়েন্টমেন্টের রেফারেন্সে আয়ের রেকর্ডটি খুঁজে বের করা
        public async Task<DoctorEarning?> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.AppointmentId == appointmentId && !e.IsDeleted);
        }

        // ডাক্তারের শেয়ার করা মোট টাকার পরিমাণ ক্যালকুলেট করা (পেইড অথবা আনপেইড ফিল্টার সহ)
        public async Task<decimal> GetTotalDoctorShareAmountAsync(Guid doctorId, bool onlyPaid)
        {
            return await _dbSet
                .Where(e => e.DoctorId == doctorId && e.IsPaid == onlyPaid && !e.IsDeleted)
                .SumAsync(e => e.DoctorShareAmount);
        }
    }
}
