using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Patients
{
    public interface IPatientReferralRepository : IGenericRepository<PatientReferral>
    {
        // ১. রেফারেল কোড দিয়ে নির্দিষ্ট রেফারেল খুঁজে বের করা
        Task<PatientReferral?> GetByReferralCodeAsync(string referralCode);

        // ২. নির্দিষ্ট একজন পেশেন্টের সব রেফারেল হিস্টোরি দেখা
        Task<IEnumerable<PatientReferral>> GetReferralsByPatientIdAsync(Guid patientId);

        // ৩. একজন ডাক্তারের পাঠানো (Outgoing) সব রেফারেল দেখা
        Task<IEnumerable<PatientReferral>> GetOutgoingReferralsByDoctorIdAsync(Guid referringDoctorId);

        // ৪. একজন ডাক্তারের কাছে আসা (Incoming) সব রেফারেল দেখা
        Task<IEnumerable<PatientReferral>> GetIncomingReferralsByDoctorIdAsync(Guid referredToDoctorId);

        // ৫. উরজেন্সি লেভেল (UrgencyLevel) অনুযায়ী পেন্ডিং রেফারেল ফিল্টার করা
        Task<IEnumerable<PatientReferral>> GetPendingUrgentReferralsAsync();

        // ৬. নির্দিষ্ট ডিপার্টমেন্ট অনুযায়ী রেফারেল লিস্ট দেখা
        Task<IEnumerable<PatientReferral>> GetReferralsByDepartmentAsync(string departmentName);
    }
}
