using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Domain.Interfaces.Wards
{
    public interface IAdmissionRepository : IGenericRepository<Admission>
    {
        // ১. অ্যাডমিশন কোড (Unique ID) দিয়ে ভর্তি তথ্য বের করা
        Task<Admission?> GetByAdmissionCodeAsync(string admissionCode, CancellationToken ct = default);

        // ২. বর্তমানে হসপিটালে ভর্তি থাকা সব পেশেন্টের লিস্ট
        Task<IEnumerable<Admission>> GetCurrentlyAdmittedPatientsAsync(CancellationToken ct = default);

        // ৩. নির্দিষ্ট একজন পেশেন্টের আগের সব ভর্তির ইতিহাস
        Task<IEnumerable<Admission>> GetPatientAdmissionHistoryAsync(Guid patientId, CancellationToken ct = default);

        // ৪. নির্দিষ্ট একজন ডাক্তারের অধীনে বর্তমানে কতজন পেশেন্ট ভর্তি আছেন
        Task<IEnumerable<Admission>> GetAdmissionsByDoctorIdAsync(Guid doctorId, CancellationToken ct = default);

        // ৫. নির্দিষ্ট একটি বেডে বর্তমানে কে ভর্তি আছেন
        Task<Admission?> GetCurrentAdmissionByBedIdAsync(Guid bedId, CancellationToken ct = default);

        // ৬. স্ট্যাটাস অনুযায়ী ফিল্টার (Admitted, Discharged, Transferred)
        Task<IEnumerable<Admission>> GetByStatusAsync(AdmissionStatus status, CancellationToken ct = default);

        // ৭. ডিসচার্জ প্রোসেস সম্পন্ন করা
        Task DischargePatientAsync(Guid admissionId, string notes, string summary, CancellationToken ct = default);
    }
}
