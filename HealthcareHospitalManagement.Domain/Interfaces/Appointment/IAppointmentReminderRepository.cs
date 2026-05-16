using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Enums.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Appointment
{
    public interface IAppointmentReminderRepository : IGenericRepository<AppointmentReminder>
    {
        // নির্দিষ্ট একটি অ্যাপয়েন্টমেন্টের সব রিমাইন্ডার লগ খুঁজে বের করা
        IAsyncEnumerable<AppointmentReminder> GetRemindersByAppointmentIdStream(Guid appointmentId);

        // বর্তমানে যে রিমাইন্ডারগুলো পেন্ডিং আছে এবং পাঠানোর সময় হয়ে গেছে সেগুলো স্ট্রীম করা
        IAsyncEnumerable<AppointmentReminder> GetPendingRemindersStream(DateTime currentTime);

        // ডেলিভারি স্ট্যাটাস (Sent, Failed, Pending) অনুযায়ী ফিল্টার করা
        IAsyncEnumerable<AppointmentReminder> GetRemindersByStatusStream(NotificationStatus status);
    }
}
