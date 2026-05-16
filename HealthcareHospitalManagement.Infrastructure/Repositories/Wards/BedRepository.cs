using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Wards
{
    public class BedRepository(ApplicationDbContext context)
        : GenericRepository<Bed>(context), IBedRepository
    {
        private readonly DbSet<Bed> _dbSet = context.Set<Bed>();

        // 🔹 ওয়ার্ড অনুযায়ী বেড লিস্ট
        public async Task<IEnumerable<Bed>> GetBedsByWardIdAsync(Guid wardId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.WardId == wardId && !b.IsDeleted)
                .OrderBy(b => b.BedNumber)
                .ToListAsync(ct);
        }

        // 🔹 খালি বেডগুলো খুঁজে পাওয়া
        public async Task<IEnumerable<Bed>> GetAvailableBedsByWardAsync(Guid wardId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.WardId == wardId &&
                            b.Status == BedStatus.Available &&
                            !b.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 স্পেশাল বেড ফিল্টারিং
        public async Task<IEnumerable<Bed>> GetBedsByFacilityAsync(bool hasOxygen, bool hasMonitor, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.HasOxygen == hasOxygen &&
                            b.HasMonitor == hasMonitor &&
                            b.Status == BedStatus.Available &&
                            !b.IsDeleted)
                .Include(b => b.Ward)
                .ToListAsync(ct);
        }

        // 🔹 ভর্তি বা ডিসচার্জের সময় স্ট্যাটাস পরিবর্তন
        public async Task UpdateBedStatusAsync(Guid bedId, BedStatus status, CancellationToken ct = default)
        {
            var bed = await GetByIdAsync(bedId, ct);
            if (bed != null)
            {
                bed.Status = status;
                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 রিয়েল-টাইম কাউন্ট
        public async Task<int> GetAvailableBedCountByWardAsync(Guid wardId, CancellationToken ct = default)
        {
            return await _dbSet
                .CountAsync(b => b.WardId == wardId &&
                                b.Status == BedStatus.Available &&
                                !b.IsDeleted, ct);
        }

        // 🔹 আইসোলেশন বেড ফিল্টার
        public async Task<IEnumerable<Bed>> GetIsolationBedsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.IsIsolation && !b.IsDeleted)
                .Include(b => b.Ward)
                .ToListAsync(ct);
        }
    }
}
