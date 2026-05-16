using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext ইনজেক্ট করা হয়েছে
    public class DoctorNoteRepository(ApplicationDbContext context)
        : GenericRepository<DoctorNote>(context), IDoctorNoteRepository
    {
        private readonly DbSet<DoctorNote> _dbSet = context.Set<DoctorNote>();

        // পেশেন্টের সব নোট স্ট্রীম করা হচ্ছে; যদি includePrivate false হয় তবে শুধু পাবলিক নোট দেখাবে
        public IAsyncEnumerable<DoctorNote> GetNotesByPatientStream(Guid patientId, bool includePrivate)
        {
            return _dbSet
                .Where(n => n.PatientId == patientId && !n.IsDeleted)
                .Where(n => includePrivate || !n.IsPrivate) // প্রাইভেসি লজিক
                .OrderByDescending(n => n.IsPinned) // পিন করা নোট আগে
                .ThenByDescending(n => n.CreatedAt) // তারপর নতুন নোট আগে
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডাক্তারের আইডি অনুযায়ী তার নিজের লেখা সব নোটের লিস্ট
        public IAsyncEnumerable<DoctorNote> GetNotesByDoctorStream(Guid doctorId)
        {
            return _dbSet
                .Where(n => n.DoctorId == doctorId && !n.IsDeleted)
                .OrderByDescending(n => n.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // অ্যাপয়েন্টমেন্ট রেফারেন্স অনুযায়ী নোট খুঁজে বের করা
        public async Task<DoctorNote?> GetNoteByAppointmentIdAsync(Guid appointmentId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(n => n.AppointmentId == appointmentId && !n.IsDeleted);
        }

        // শুধুমাত্র গুরুত্বপূর্ণ বা পিন করা নোটগুলো ফিল্টার করা
        public IAsyncEnumerable<DoctorNote> GetPinnedNotesByPatientStream(Guid patientId)
        {
            return _dbSet
                .Where(n => n.PatientId == patientId && n.IsPinned && !n.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ট্যাগ (যেমন: "Critical") অনুযায়ী পেশেন্টের নোট সার্চ করা
        public IAsyncEnumerable<DoctorNote> SearchNotesByTagStream(Guid patientId, string tag)
        {
            return _dbSet
                .Where(n => n.PatientId == patientId && n.Tags != null && n.Tags.Contains(tag) && !n.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
