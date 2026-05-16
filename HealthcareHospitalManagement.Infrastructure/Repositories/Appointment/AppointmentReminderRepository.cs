using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Interfaces.Appointment;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Appointment
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে পাস করা হয়েছে
    public class AppointmentReminderRepository(ApplicationDbContext context)
        : GenericRepository<AppointmentReminder>(context), IAppointmentReminderRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<AppointmentReminder> _dbSet = context.Set<AppointmentReminder>();

        // নির্দিষ্ট অ্যাপয়েন্টমেন্ট আইডির ওপর ভিত্তি করে সব রিমাইন্ডার লগ স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<AppointmentReminder> GetRemindersByAppointmentIdStream(Guid appointmentId)
        {
            return _dbSet
                .Where(r => r.AppointmentId == appointmentId && !r.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // শিডিউল টাইম হয়ে গেছে এমন পেন্ডিং রিমাইন্ডারগুলো খোঁজা (Background Job এর জন্য উপযোগী)
        public IAsyncEnumerable<AppointmentReminder> GetPendingRemindersStream(DateTime currentTime)
        {
            return _dbSet
                .Where(r => r.Status == NotificationStatus.Pending
                         && r.ScheduledAt <= currentTime
                         && !r.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // স্ট্যাটাস ফিল্টার করে ডেটাগুলো স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<AppointmentReminder> GetRemindersByStatusStream(NotificationStatus status)
        {
            return _dbSet
                .Where(r => r.Status == status && !r.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
