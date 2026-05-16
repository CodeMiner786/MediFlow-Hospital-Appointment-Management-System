using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class RoleAssignmentLogRepository(ApplicationDbContext context)
        : GenericRepository<RoleAssignmentLog>(context), IRoleAssignmentLogRepository
    {
        private readonly DbSet<RoleAssignmentLog> _dbSet = context.Set<RoleAssignmentLog>();

        // ১. নির্দিষ্ট ইউজারের রোল পরিবর্তনের ইতিহাস (UserId ব্যবহার করে)
        public IAsyncEnumerable<RoleAssignmentLog> GetLogsByTargetUserStream(Guid userId)
        {
            return _dbSet
                .AsNoTracking()
                .Where(log => log.UserId == userId) // Entity-তে নাম UserId
                .OrderByDescending(log => log.AssignedAt)
                .AsAsyncEnumerable();
        }

        // ২. কে পরিবর্তন করেছেন (AssignedByUserId ব্যবহার করে)
        public async Task<IEnumerable<RoleAssignmentLog>> GetLogsByAdminAsync(Guid adminUserId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(log => log.AssignedByUserId == adminUserId) // Entity-তে নাম AssignedByUserId
                .OrderByDescending(log => log.AssignedAt)
                .ToListAsync();
        }

        // ৩. নির্দিষ্ট সময়ের রিপোর্ট
        public async Task<IEnumerable<RoleAssignmentLog>> GetLogsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(log => log.AssignedAt >= start && log.AssignedAt <= end)
                .OrderByDescending(log => log.AssignedAt)
                .ToListAsync();
        }

        // ৪. নির্দিষ্ট রোলের কাউন্ট (ToRole ব্যবহার করে)
        public async Task<int> GetCountByTargetRoleAsync(UserRole role)
        {
            // যেহেতু Entity-তে ToRole অলরেডি UserRole টাইপ, তাই সরাসরি তুলনা করা যাবে
            return await _dbSet
                .CountAsync(log => log.ToRole == role); // Entity-তে নাম ToRole
        }
    }
}