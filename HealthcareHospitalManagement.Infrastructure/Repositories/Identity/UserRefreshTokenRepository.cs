using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Identity;

public class UserRefreshTokenRepository(ApplicationDbContext context)
    : GenericRepository<UserRefreshToken>(context), IUserRefreshTokenRepository
{
    public async Task<IEnumerable<UserRefreshToken>> GetByUserIdAsync(Guid userId)
    {
        return await context.UserRefreshTokens
            .Where(rt => rt.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserRefreshToken?> GetByTokenAsync(string token)
    {
        return await context.UserRefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<UserRefreshToken?> GetByTokenAndJwtIdAsync(string token, string jwtId)
    {
        return await context.UserRefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token && rt.JwtId == jwtId);
    }

    public async Task<IEnumerable<UserRefreshToken>> GetActiveTokensByUserIdAsync(Guid userId)
    {
        var now = DateTime.UtcNow;
        return await context.UserRefreshTokens
            .Where(rt => rt.UserId == userId
                         && !rt.IsRevoked
                         && rt.ExpiresAt > now)
            .ToListAsync();
    }

    public async Task RevokeAllTokensAsync(Guid userId)
    {
        var tokens = await context.UserRefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        foreach (var token in tokens)
            token.IsRevoked = true;

        await context.SaveChangesAsync();
    }

    public async Task DeleteInactiveTokensAsync(Guid userId)
    {
        var now = DateTime.UtcNow;
        var inactiveTokens = await context.UserRefreshTokens
            .Where(rt => rt.UserId == userId && (rt.IsRevoked || rt.ExpiresAt <= now))
            .ToListAsync();

        context.UserRefreshTokens.RemoveRange(inactiveTokens);
        await context.SaveChangesAsync();
    }
}