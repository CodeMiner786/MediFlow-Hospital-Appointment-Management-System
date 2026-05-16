using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Permission;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class UserPermissionOverrideRepository(ApplicationDbContext context)
         : GenericRepository<UserPermissionOverride>(context), IUserPermissionOverrideRepository
    {
        private readonly DbSet<UserPermissionOverride> _dbSet = context.Set<UserPermissionOverride>();

        // 🔹 ইউজারের পারমিশন চেক করার সময় এটি প্রথমে কল হবে
        public async Task<IEnumerable<UserPermissionOverride>> GetOverridesByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(upo => upo.UserId == userId && !upo.IsDeleted)
                .ToListAsync();
        }

        // 🔹 স্পেসিফিক মডিউল ও অ্যাকশন অনুযায়ী ওভাররাইড খুঁজে বের করা
        public async Task<UserPermissionOverride?> GetOverrideAsync(Guid userId, string module, PermissionAction action)
        {
            return await _dbSet.FirstOrDefaultAsync(upo =>
                upo.UserId == userId &&
                upo.Module.ToLower() == module.ToLower() &&
                upo.Action == action &&
                !upo.IsDeleted);
        }

        // 🔹 ইউজারকে আবার ডিফল্ট রোল পারমিশনে ফিরিয়ে নিতে সব ওভাররাইড মুছে ফেলা
        public async Task RemoveAllOverridesAsync(Guid userId)
        {
            var overrides = await _dbSet.Where(upo => upo.UserId == userId).ToListAsync();
            if (overrides.Any())
            {
                _dbSet.RemoveRange(overrides);
                await context.SaveChangesAsync();
            }
        }

        // 🔹 অডিটের জন্য: কোন মডিউলে কারা কারা স্পেশাল এক্সেস নিয়ে বসে আছে
        public async Task<IEnumerable<UserPermissionOverride>> GetUsersByModuleOverrideAsync(string module)
        {
            return await _dbSet
                .Where(upo => upo.Module.ToLower() == module.ToLower() && !upo.IsDeleted)
                .Include(upo => upo.User)
                .ToListAsync();
        }
    }
}
