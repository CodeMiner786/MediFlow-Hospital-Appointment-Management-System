using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorNoteRepository : IGenericRepository<DoctorNote>
    {
        // নির্দিষ্ট একজন পেশেন্টের সব নোট স্ট্রীম করা (প্রাইভেসি ফিল্টার সহ)
        IAsyncEnumerable<DoctorNote> GetNotesByPatientStream(Guid patientId, bool includePrivate);

        // নির্দিষ্ট একজন ডাক্তারের লেখা সব নোট খুঁজে বের করা
        IAsyncEnumerable<DoctorNote> GetNotesByDoctorStream(Guid doctorId);

        // অ্যাপয়েন্টমেন্ট আইডি দিয়ে ওই ভিজিটের নির্দিষ্ট নোটটি খুঁজে বের করা
        Task<DoctorNote?> GetNoteByAppointmentIdAsync(Guid appointmentId);

        // গুরুত্বপূর্ণ (Pinned) নোটগুলো সবার আগে দেখানোর জন্য স্ট্রীম করা
        IAsyncEnumerable<DoctorNote> GetPinnedNotesByPatientStream(Guid patientId);

        // ট্যাগ (Tags) অনুযায়ী নোট সার্চ করা
        IAsyncEnumerable<DoctorNote> SearchNotesByTagStream(Guid patientId, string tag);
    }
}
