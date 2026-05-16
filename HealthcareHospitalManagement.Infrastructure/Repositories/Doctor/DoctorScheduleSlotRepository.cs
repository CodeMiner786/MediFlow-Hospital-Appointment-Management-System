using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    public class DoctorScheduleSlotRepository(ApplicationDbContext context)
        : GenericRepository<DoctorScheduleSlot>(context), IDoctorScheduleSlotRepository
    {
        private readonly DbSet<DoctorScheduleSlot> _dbSet = context.Set<DoctorScheduleSlot>();

        public IAsyncEnumerable<DoctorScheduleSlot> GetSlotsByDoctorAndDateStream(Guid doctorId, DateTime date)
        {
            return _dbSet
                .Where(s => s.DoctorId == doctorId && s.SlotDate.Date == date.Date && !s.IsDeleted)
                .OrderBy(s => s.SlotStartTime)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<DoctorScheduleSlot> GetAvailableSlotsStream(Guid doctorId, DateTime date)
        {
            return _dbSet
                .Where(s => s.DoctorId == doctorId
                         && s.SlotDate.Date == date.Date
                         && !s.IsBooked
                         && !s.IsBlocked
                         && !s.IsDeleted)
                .OrderBy(s => s.SlotStartTime)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task<IEnumerable<DoctorScheduleSlot>> GetSlotsByScheduleIdAsync(Guid scheduleId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(s => s.DoctorScheduleId == scheduleId && !s.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<bool> MarkSlotAsBookedAsync(Guid slotId, Guid appointmentId, CancellationToken ct = default)
        {
            var slot = await GetByIdAsync(slotId, ct);
            if (slot == null || slot.IsBooked || slot.IsBlocked) return false;

            slot.IsBooked = true;
            slot.AppointmentId = appointmentId;

            // 🔹 এখন CancellationToken pass করা হচ্ছে
            await UpdateAsync(slot, ct);
            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task MarkSlotAsBlockedAsync(Guid slotId, string reason, CancellationToken ct = default)
        {
            var slot = await GetByIdAsync(slotId, ct);
            if (slot != null)
            {
                slot.IsBlocked = true;
                slot.BlockReason = reason;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(slot, ct);
                await context.SaveChangesAsync(ct);
            }
        }

        public async Task<bool> IsSlotAvailableAsync(Guid slotId, CancellationToken ct = default)
        {
            return await _dbSet.AnyAsync(s => s.Id == slotId && !s.IsBooked && !s.IsBlocked && !s.IsDeleted, ct);
        }
    }
}
