using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    public class DoctorLeaveRepository(ApplicationDbContext context)
        : GenericRepository<DoctorLeave>(context), IDoctorLeaveRepository
    {
        private readonly DbSet<DoctorLeave> _dbSet = context.Set<DoctorLeave>();

        public IAsyncEnumerable<DoctorLeave> GetLeavesByDoctorIdStream(Guid doctorId)
        {
            return _dbSet
                .Where(l => l.DoctorId == doctorId && !l.IsDeleted)
                .OrderByDescending(l => l.LeaveFrom)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<DoctorLeave> GetPendingLeavesStream()
        {
            return _dbSet
                .Where(l => !l.IsApproved && l.ApprovedAt == null && !l.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<DoctorLeave> GetLeavesByDateRangeStream(DateTime start, DateTime end)
        {
            return _dbSet
                .Where(l => l.LeaveFrom <= end && l.LeaveTo >= start && !l.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task<bool> IsDoctorOnLeaveAsync(Guid doctorId, DateTime date, CancellationToken ct = default)
        {
            return await _dbSet.AnyAsync(l =>
                l.DoctorId == doctorId &&
                l.IsApproved &&
                date >= l.LeaveFrom &&
                date <= l.LeaveTo &&
                !l.IsDeleted, ct);
        }

        public async Task UpdateLeaveStatusAsync(Guid leaveId, bool isApproved, string processorName, string? rejectionReason = null, CancellationToken ct = default)
        {
            var leave = await GetByIdAsync(leaveId, ct);
            if (leave != null)
            {
                leave.IsApproved = isApproved;
                leave.ApprovedAt = DateTime.UtcNow;
                leave.ApprovedBy = processorName;
                leave.RejectionReason = rejectionReason;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(leave, ct);
                await context.SaveChangesAsync(ct);
            }
        }
    }
}
