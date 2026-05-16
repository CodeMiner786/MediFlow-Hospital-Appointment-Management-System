using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorUnavailabilityRepository : IGenericRepository<DoctorUnavailability>
    {
        // নির্দিষ্ট একজন ডাক্তারের সব অনুপস্থিতির রেকর্ড স্ট্রীম করা
        IAsyncEnumerable<DoctorUnavailability> GetUnavailabilitiesByDoctorStream(Guid doctorId);

        // নির্দিষ্ট একটি তারিখের সব অনুপস্থিতি খুঁজে বের করা
        IAsyncEnumerable<DoctorUnavailability> GetUnavailabilitiesByDateStream(Guid doctorId, DateTime date);

        // ডাক্তার বর্তমানে (এই মুহূর্তে) অনুপস্থিত কি না তা যাচাই করা
        Task<bool> IsDoctorUnavailableNowAsync(Guid doctorId);

        // নির্দিষ্ট সময়ের ব্যবধানে ডাক্তারের কোনো অনুপস্থিতি রেকর্ড আছে কি না (স্লট ব্লকিংয়ের জন্য)
        Task<bool> CheckConflictAsync(Guid doctorId, DateTime date, TimeOnly? fromTime, TimeOnly? toTime);
    }
}
