using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
using HealthcareHospitalManagement.Domain.Enums.Telemedicine;

namespace HealthcareHospitalManagement.Domain.Interfaces.Telemedicine
{
    public interface ITelemedicineSessionRepository : IGenericRepository<TelemedicineSession>
    {
        // ১. অ্যাপয়েন্টমেন্ট আইডি দিয়ে সেশন খুঁজে বের করা
        Task<TelemedicineSession?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken ct = default);

        // ২. সেশন কোড (Unique Code) দিয়ে সেশন ডিটেইলস দেখা
        Task<TelemedicineSession?> GetBySessionCodeAsync(string sessionCode, CancellationToken ct = default);

        // ৩. ডক্টরের আজকের সব টেলিমেডিসিন সেশনগুলো দেখা
        Task<IEnumerable<TelemedicineSession>> GetDoctorsTodaysSessionsAsync(Guid doctorId, CancellationToken ct = default);

        // ৪. পেশেন্টের আগের সব টেলিমেডিসিন হিস্টোরি
        Task<IEnumerable<TelemedicineSession>> GetPatientSessionHistoryAsync(Guid patientId, CancellationToken ct = default);

        // ৫. স্ট্যাটাস অনুযায়ী সেশন ফিল্টার করা (e.g., Scheduled, Live, Completed)
        Task<IEnumerable<TelemedicineSession>> GetSessionsByStatusAsync(TelemedicineSessionStatus status, CancellationToken ct = default);

        // ৬. সেশন শুরু এবং শেষ হওয়ার সময় আপডেট করা
        Task StartSessionAsync(Guid sessionId, CancellationToken ct = default);
        Task EndSessionAsync(Guid sessionId, int durationMinutes, CancellationToken ct = default);
    }
}
