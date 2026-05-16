using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Patients
{
    public interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
    {
        // ১. নির্দিষ্ট একজন পেশেন্টের পুরো মেডিকেল হিস্টোরি দেখা
        Task<IEnumerable<MedicalRecord>> GetRecordsByPatientIdAsync(Guid patientId);

        // ২. একজন ডক্টর তার আন্ডারে থাকা পেশেন্টদের কি কি রেকর্ড লিখেছেন
        Task<IEnumerable<MedicalRecord>> GetRecordsByDoctorIdAsync(Guid doctorId);

        // ৩. নির্দিষ্ট রোগ (Diagnosis) অনুযায়ী রেকর্ড ফিল্টার করা (Research এর জন্য)
        Task<IEnumerable<MedicalRecord>> GetRecordsByDiagnosisAsync(string diagnosis);

        // ৪. ফলো-আপ ডেট অনুযায়ী পেশেন্টদের লিস্ট বের করা
        Task<IEnumerable<MedicalRecord>> GetUpcomingFollowUpsAsync(DateTime date);

        // ৫. পেশেন্টের লেটেস্ট মেডিকেল রেকর্ড বা ভিজিট দেখা
        Task<MedicalRecord?> GetLatestRecordByPatientIdAsync(Guid patientId);
    }
}
