using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorScheduleRepository : IGenericRepository<DoctorSchedule>
    {
        // নির্দিষ্ট একজন ডাক্তারের পুরো সপ্তাহের শিডিউল স্ট্রীম করা
        IAsyncEnumerable<DoctorSchedule> GetSchedulesByDoctorStream(Guid doctorId);

        // নির্দিষ্ট কোনো একদিনে (যেমন: Monday) ডাক্তারের শিডিউল আছে কিনা তা দেখা
        IAsyncEnumerable<DoctorSchedule> GetSchedulesByDayStream(Guid doctorId, DayOfWeek day);

        // বর্তমানে একটিভ আছে এমন শিডিউলগুলো খুঁজে বের করা (Effective Date চেক করে)
        IAsyncEnumerable<DoctorSchedule> GetActiveSchedulesStream(Guid doctorId);

        // শিডিউলের সাথে তার অধীনে থাকা সব স্লট (Slots) একসাথে লোড করা
        Task<DoctorSchedule?> GetScheduleWithSlotsAsync(Guid scheduleId);

        // কোনো নির্দিষ্ট সময়ে ডাক্তারের শিডিউল ওভারল্যাপ হচ্ছে কিনা তা যাচাই করা
        Task<bool> HasScheduleOverlapAsync(Guid doctorId, DayOfWeek day, TimeOnly start, TimeOnly end);
    }
}
