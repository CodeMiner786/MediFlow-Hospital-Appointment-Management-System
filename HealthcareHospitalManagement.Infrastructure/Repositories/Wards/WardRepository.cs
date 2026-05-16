using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Wards
{
    public class WardRepository(ApplicationDbContext context)
        : GenericRepository<Ward>(context), IWardRepository
    {
        private readonly DbSet<Ward> _dbSet = context.Set<Ward>();

        // 🔹 ইউনিক ওয়ার্ড কোড এবং তার অধীনে থাকা বেডগুলোর লিস্টসহ লোড করা
        public async Task<Ward?> GetByCodeAsync(string wardCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(w => w.Beds)
                .FirstOrDefaultAsync(w => w.WardCode == wardCode && !w.IsDeleted, ct);
        }

        // 🔹 হসপিটালের ফ্লোর প্ল্যান অনুযায়ী ওয়ার্ড লিস্ট
        public async Task<IEnumerable<Ward>> GetWardsByFloorAsync(int floorNumber, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(w => w.FloorNumber == floorNumber && !w.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 নির্দিষ্ট ক্যাটাগরির ওয়ার্ড খোঁজা (অ্যাডমিন ড্যাশবোর্ডের জন্য)
        public async Task<IEnumerable<Ward>> GetWardsByTypeAsync(WardType wardType, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(w => w.WardType == wardType && !w.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 ইমারজেন্সি পেশেন্ট ভর্তির সময় খালি সিট আছে এমন ওয়ার্ডগুলো দেখানো
        public async Task<IEnumerable<Ward>> GetWardsWithAvailableBedsAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(w => w.AvailableBeds > 0 && !w.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 বেড টেবিলে কোনো পরিবর্তন হলে ওয়ার্ডের কাউন্ট সিঙ্ক করা
        public async Task UpdateBedCountsAsync(Guid wardId, CancellationToken ct = default)
        {
            var ward = await GetByIdAsync(wardId, ct);
            if (ward != null)
            {
                var total = await context.Set<Bed>().CountAsync(b => b.WardId == wardId && !b.IsDeleted, ct);
                var available = await context.Set<Bed>().CountAsync(b => b.WardId == wardId &&
                                                                    b.Status == BedStatus.Available &&
                                                                    !b.IsDeleted, ct);

                ward.TotalBeds = total;
                ward.AvailableBeds = available;

                await context.SaveChangesAsync(ct);
            }
        }

        // 🔹 বড় হসপিটালের ক্ষেত্রে মাল্টিপল বিল্ডিং ম্যানেজমেন্ট
        public async Task<IEnumerable<Ward>> GetWardsByBuildingAsync(string buildingName, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(w => w.Building != null &&
                            string.Equals(w.Building, buildingName, StringComparison.OrdinalIgnoreCase) &&
                            !w.IsDeleted)
                .ToListAsync(ct);
        }
    }
}
