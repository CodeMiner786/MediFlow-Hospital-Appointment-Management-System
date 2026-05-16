using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Patients
{
    public interface IPatientVitalRepository : IGenericRepository<PatientVital>
    {
        // ১. নির্দিষ্ট একজন পেশেন্টের সব ভাইটাল হিস্টোরি দেখা
        Task<IEnumerable<PatientVital>> GetVitalsByPatientIdAsync(Guid patientId);

        // ২. পেশেন্টের একদম লেটেস্ট (Current) ভাইটাল রেকর্ডটি দেখা
        Task<PatientVital?> GetLatestVitalsByPatientIdAsync(Guid patientId);

        // ৩. নির্দিষ্ট ডেট রেঞ্জে ভাইটাল সাইন ফিল্টার করা (Chart তৈরির জন্য)
        Task<IEnumerable<PatientVital>> GetVitalsByDateRangeAsync(Guid patientId, DateTime start, DateTime end);

        // ৪. ক্রিটিক্যাল ভাইটাল সাইন (অস্বাভাবিক) রেকর্ডগুলো খুঁজে বের করা
        Task<IEnumerable<PatientVital>> GetCriticalVitalsAsync(Guid patientId);

        // ৫. কে ভাইটাল রেকর্ড করেছে তার নাম দিয়ে ফিল্টার করা (Auditing)
        Task<IEnumerable<PatientVital>> GetVitalsByRecorderNameAsync(string recorderName);
        
    }
}
