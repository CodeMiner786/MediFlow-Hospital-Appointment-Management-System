using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorAvailabilityLogRepository : IGenericRepository<DoctorAvailabilityLog>
    {
        // নির্দিষ্ট একজন ডাক্তারের সব অ্যাভেইল্যাবিলিটি লগ স্ট্রীম আকারে পাওয়া
        IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByDoctorIdStream(Guid doctorId);

        // নির্দিষ্ট একটি স্ট্যাটাস (যেমন: OnLeave, Available) অনুযায়ী লগগুলো ফিল্টার করা
        IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByStatusStream(DoctorAvailabilityStatus status);

        // নির্দিষ্ট সময়ের ব্যবধানে হওয়া সব পরিবর্তনের লগ স্ট্রীম করা (Audit Purpose)
        IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByDateRangeStream(DateTime start, DateTime end);

        // একজন ডাক্তারের সর্বশেষ স্ট্যাটাস পরিবর্তনটি খুঁজে বের করা
        Task<DoctorAvailabilityLog?> GetLatestLogByDoctorIdAsync(Guid doctorId);
    }
}
