using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    public class StaffAttendanceRepository(ApplicationDbContext context)
        : GenericRepository<StaffAttendance>(context), IStaffAttendanceRepository
    {
        private readonly DbSet<StaffAttendance> _dbSet = context.Set<StaffAttendance>();

        public IAsyncEnumerable<StaffAttendance> GetAttendanceByStaffStream(Guid staffId)
        {
            return _dbSet
                .Where(a => a.StaffId == staffId && !a.IsDeleted)
                .OrderByDescending(a => a.AttendanceDate)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<StaffAttendance> GetDailyAttendanceStream(DateOnly date)
        {
            return _dbSet
                .Where(a => a.AttendanceDate == date && !a.IsDeleted)
                .Include(a => a.Staff)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<StaffAttendance> GetMonthlyAttendanceStream(Guid staffId, int month, int year)
        {
            return _dbSet
                .Where(a => a.StaffId == staffId
                         && a.AttendanceDate.Month == month
                         && a.AttendanceDate.Year == year
                         && !a.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task UpdateCheckInAsync(Guid staffId, DateOnly date, TimeOnly checkInTime, CancellationToken ct = default)
        {
            var attendance = await _dbSet.FirstOrDefaultAsync(a => a.StaffId == staffId && a.AttendanceDate == date, ct);

            if (attendance == null)
            {
                await AddAsync(new StaffAttendance
                {
                    StaffId = staffId,
                    AttendanceDate = date,
                    CheckInTime = checkInTime,
                    IsPresent = true
                }, ct);
            }
            else
            {
                attendance.CheckInTime = checkInTime;
                attendance.IsPresent = true;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(attendance, ct);
            }
        }

        public async Task UpdateCheckOutAsync(Guid staffId, DateOnly date, TimeOnly checkOutTime, CancellationToken ct = default)
        {
            var attendance = await _dbSet.FirstOrDefaultAsync(a => a.StaffId == staffId && a.AttendanceDate == date, ct);
            if (attendance != null)
            {
                attendance.CheckOutTime = checkOutTime;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(attendance, ct);
            }
        }

        public async Task<StaffAttendance?> GetTodayAttendanceAsync(Guid staffId, CancellationToken ct = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await _dbSet.FirstOrDefaultAsync(a => a.StaffId == staffId && a.AttendanceDate == today && !a.IsDeleted, ct);
        }
    }
}
