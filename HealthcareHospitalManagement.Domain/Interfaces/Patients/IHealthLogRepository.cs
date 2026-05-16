using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Patients
{
    public interface IHealthLogRepository : IGenericRepository<HealthLog>
    {
        // ১. নির্দিষ্ট একজন পেশেন্টের সব হেলথ লগ দেখা
        Task<IEnumerable<HealthLog>> GetLogsByPatientIdAsync(Guid patientId);

        // ২. ইউজারের লেটেস্ট ভাইটালস (Vitals) দেখা (Dashboard এর জন্য)
        Task<HealthLog?> GetLatestLogByPatientIdAsync(Guid patientId);

        // ৩. নির্দিষ্ট সময়ের মধ্যে পেশেন্টের হেলথ ডেটা (Reporting/Graphing এর জন্য)
        Task<IEnumerable<HealthLog>> GetLogsByDateRangeAsync(Guid patientId, DateTime start, DateTime end);

        // ৪. অস্বাভাবিক ভাইটালস ফিল্টার করা (e.g. High BP or Low Oxygen)
        Task<IEnumerable<HealthLog>> GetAbnormalLogsAsync(Guid patientId);

        // ৫. সোর্স অনুযায়ী ফিল্টার (যেমন: শুধুমাত্র Smartwatch থেকে আসা ডেটা)
        Task<IEnumerable<HealthLog>> GetLogsBySourceAsync(Guid patientId, string source);
    }
}
