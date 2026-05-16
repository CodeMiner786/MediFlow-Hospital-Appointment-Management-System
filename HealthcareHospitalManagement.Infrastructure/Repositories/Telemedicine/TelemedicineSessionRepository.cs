using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
using HealthcareHospitalManagement.Domain.Enums.Telemedicine;
using HealthcareHospitalManagement.Domain.Interfaces.Telemedicine;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Telemedicine
{
    public class TelemedicineSessionRepository(ApplicationDbContext context)
         : GenericRepository<TelemedicineSession>(context), ITelemedicineSessionRepository
    {
        private readonly DbSet<TelemedicineSession> _dbSet = context.Set<TelemedicineSession>();

        // 🔹 অ্যাপয়েন্টমেন্ট থেকে সরাসরি সেশন ডাটা লোড করা
        public async Task<TelemedicineSession?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.Patient)
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.AppointmentId == appointmentId && !s.IsDeleted, ct);
        }

        // 🔹 ইউনিক সেশন কোড দিয়ে মিটিং জয়েন করার জন্য
        public async Task<TelemedicineSession?> GetBySessionCodeAsync(string sessionCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.Appointment)
                .FirstOrDefaultAsync(s => s.SessionCode == sessionCode && !s.IsDeleted, ct);
        }

        // 🔹 ডক্টরের আজকের শিডিউল
        public async Task<IEnumerable<TelemedicineSession>> GetDoctorsTodaysSessionsAsync(Guid doctorId, CancellationToken ct = default)
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet
                .Where(s => s.DoctorId == doctorId &&
                            s.ScheduledAt.Date == today &&
                            !s.IsDeleted)
                .Include(s => s.Patient)
                .OrderBy(s => s.ScheduledAt)
                .ToListAsync(ct);
        }

        // 🔹 পেশেন্টের পুরনো সেশন রেকর্ড
        public async Task<IEnumerable<TelemedicineSession>> GetPatientSessionHistoryAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.PatientId == patientId && !s.IsDeleted)
                .OrderByDescending(s => s.ScheduledAt)
                .ToListAsync(ct);
        }

        // 🔹 অ্যাডমিন প্যানেলের জন্য স্ট্যাটাস ফিল্টার
        public async Task<IEnumerable<TelemedicineSession>> GetSessionsByStatusAsync(TelemedicineSessionStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.Status == status && !s.IsDeleted)
                .Include(s => s.Doctor)
                .Include(s => s.Patient)
                .ToListAsync(ct);
        }

        // 🔹 কল শুরু হলে টাইমস্ট্যাম্প আপডেট
        public async Task StartSessionAsync(Guid sessionId, CancellationToken ct = default)
        {
            var session = await GetByIdAsync(sessionId, ct);
            if (session != null)
            {
                session.StartedAt = DateTime.UtcNow;
                session.Status = TelemedicineSessionStatus.Live;
                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 কল শেষ হলে ডিউরেশন এবং স্ট্যাটাস আপডেট
        public async Task EndSessionAsync(Guid sessionId, int durationMinutes, CancellationToken ct = default)
        {
            var session = await GetByIdAsync(sessionId, ct);
            if (session != null)
            {
                session.EndedAt = DateTime.UtcNow;
                session.DurationMinutes = durationMinutes;
                session.Status = TelemedicineSessionStatus.Completed;
                await context.SaveChangesAsync(ct);
            }
        }
    }
}
