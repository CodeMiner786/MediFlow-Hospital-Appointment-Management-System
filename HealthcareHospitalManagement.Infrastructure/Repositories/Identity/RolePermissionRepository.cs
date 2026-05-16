using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Permission;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class RolePermissionRepository(ApplicationDbContext context)
        : GenericRepository<RolePermission>(context), IRolePermissionRepository
    {
        private readonly DbSet<RolePermission> _dbSet = context.Set<RolePermission>();

        // 🔹 নির্দিষ্ট রোলের সব পারমিশন বের করা (Caching এর জন্য এটি খুব কার্যকর)
        public async Task<IEnumerable<RolePermission>> GetPermissionsByRoleAsync(UserRole role)
        {
            return await _dbSet
                .Where(rp => rp.Role == role && !rp.IsDeleted)
                .ToListAsync();
        }

        // 🔹 অথরাইজেশন চেক: এই রোলের কি এই মডিউলে এই কাজ করার অনুমতি আছে?
        public async Task<bool> HasPermissionAsync(UserRole role, string module, PermissionAction action)
        {
            return await _dbSet.AnyAsync(rp =>
                rp.Role == role &&
                rp.Module.ToLower() == module.ToLower() &&
                rp.Action == action &&
                rp.IsGranted &&
                !rp.IsDeleted);
        }

        // 🔹 বাল্ক অপারেশন: একটি রোলের সব পারমিশন রিমুভ করা
        public async Task RemovePermissionsByRoleAsync(UserRole role)
        {
            var permissions = await _dbSet.Where(rp => rp.Role == role).ToListAsync();
            if (permissions.Any())
            {
                _dbSet.RemoveRange(permissions);
                await context.SaveChangesAsync();
            }
        }

        // 🔹 মডিউল ভিত্তিক অডিট: এই মডিউলে কারা কারা এক্সেস পাচ্ছে তা দেখা
        public async Task<IEnumerable<RolePermission>> GetPermissionsByModuleAsync(string module)
        {
            return await _dbSet
                .Where(rp => rp.Module.ToLower() == module.ToLower() && !rp.IsDeleted)
                .ToListAsync();
        }
    }
}
