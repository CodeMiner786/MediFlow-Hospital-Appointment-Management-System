using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Account;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity;

public class UserRepository(ApplicationDbContext context)
    : GenericRepository<ApplicationUser>(context), IUserRepository
{
    private readonly DbSet<ApplicationUser> _dbSet = context.Set<ApplicationUser>();

    public async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        var normalizedEmail = email.ToUpper(); // ✅ Fix — string.Equals() সরিয়ে এটা দিলাম
        return await _dbSet.FirstOrDefaultAsync(u =>
            u.NormalizedEmail == normalizedEmail
            && !u.IsDeleted, ct);
    }

    public async Task<IEnumerable<ApplicationUser>> GetByRoleAsync(UserRole role, CancellationToken ct = default)
    {
        return await _dbSet
            .Where(u => u.Role == role && !u.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task<bool> IsAccountLockedAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await GetByIdAsync(userId, ct);
        return user != null && user.IsLockedOut;
    }

    public async Task UnlockUserAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await GetByIdAsync(userId, ct);
        if (user is null) return;

        user.LockoutEndAt = null;
        user.FailedLoginAttempts = 0;

        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateLoginActivityAsync(Guid userId, string ipAddress, CancellationToken ct = default)
    {
        var user = await GetByIdAsync(userId, ct);
        if (user is null) return;

        user.LastLoginAt = DateTime.UtcNow;
        user.LastLoginIp = ipAddress;
        user.FailedLoginAttempts = 0;

        await context.SaveChangesAsync(ct);
    }

    public IAsyncEnumerable<ApplicationUser> GetActiveUsersStream()
    {
        return _dbSet
            .Where(u => u.AccountStatus == AccountStatus.Active && !u.IsDeleted)
            .AsNoTracking()
            .AsAsyncEnumerable();
    }
}