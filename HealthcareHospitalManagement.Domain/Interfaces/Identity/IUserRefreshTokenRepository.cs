using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity;

public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
{
    Task<IEnumerable<UserRefreshToken>> GetByUserIdAsync(Guid userId);
    Task<UserRefreshToken?> GetByTokenAndJwtIdAsync(string token, string jwtId);
    Task<UserRefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<UserRefreshToken>> GetActiveTokensByUserIdAsync(Guid userId);
    Task RevokeAllTokensAsync(Guid userId);
    Task DeleteInactiveTokensAsync(Guid userId);
    // ✅ AddAsync সরানো হয়েছে
}