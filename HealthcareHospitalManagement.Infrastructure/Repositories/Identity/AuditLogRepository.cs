using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity
{
    public class AuditLogRepository(ApplicationDbContext context)
        : GenericRepository<AuditLog>(context), IAuditLogRepository
    {
        private readonly DbSet<AuditLog> _dbSet = context.Set<AuditLog>();

        public async Task<PagedResponse<AuditLog>> GetFilteredLogsAsync(
            Guid? userId,
            AuditAction? action,
            UserRole? role,
            DateTime? from,
            DateTime? to,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            var query = GetQueryable();

            if (userId.HasValue)
                query = query.Where(l => l.UserId == userId);

            if (action.HasValue)
                query = query.Where(l => l.Action == action.Value);

            if (role.HasValue)
            {
                var roleName = role.Value.ToString();
                query = query.Where(l => l.EntityName.Contains(roleName));
            }

            if (from.HasValue)
                query = query.Where(l => l.Timestamp >= from.Value);

            if (to.HasValue)
                query = query.Where(l => l.Timestamp <= to.Value);

            query = query.OrderByDescending(l => l.Timestamp);

            // 🔹 এখন CancellationToken pass করা হচ্ছে
            return await ToPagedAsync(query, pageNumber, pageSize, ct);
        }

        public IAsyncEnumerable<AuditLog> GetByUserIdStream(Guid userId)
        {
            return _dbSet.Where(l => l.UserId == userId)
                         .OrderByDescending(l => l.Timestamp)
                         .AsNoTracking()
                         .AsAsyncEnumerable();
        }

        public async Task<IEnumerable<AuditLog>> GetByEntityAsync(string entityName, string entityId, CancellationToken ct = default)
        {
            return await _dbSet.Where(l => l.EntityName == entityName && l.EntityId == entityId)
                               .OrderByDescending(l => l.Timestamp)
                               .ToListAsync(ct);
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
        {
            return await _dbSet.Where(l => l.Timestamp >= startDate && l.Timestamp <= endDate)
                               .OrderByDescending(l => l.Timestamp)
                               .ToListAsync(ct);
        }

        public async Task<IEnumerable<AuditLog>> GetFailedLogsAsync(CancellationToken ct = default)
        {
            return await _dbSet.Where(l => !l.IsSuccess)
                               .OrderByDescending(l => l.Timestamp)
                               .ToListAsync(ct);
        }

        public async Task DeleteLogsOlderThanAsync(DateTime date, CancellationToken ct = default)
        {
            var oldLogs = _dbSet.Where(l => l.Timestamp < date);
            _dbSet.RemoveRange(oldLogs);
            await context.SaveChangesAsync(ct);
        }
    }
}
