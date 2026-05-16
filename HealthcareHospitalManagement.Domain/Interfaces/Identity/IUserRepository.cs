using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<IEnumerable<ApplicationUser>> GetByRoleAsync(UserRole role, CancellationToken ct = default);
        Task<bool> IsAccountLockedAsync(Guid userId, CancellationToken ct = default);
        Task UnlockUserAsync(Guid userId, CancellationToken ct = default);
        Task UpdateLoginActivityAsync(Guid userId, string ipAddress, CancellationToken ct = default);
        IAsyncEnumerable<ApplicationUser> GetActiveUsersStream();
    }
}
