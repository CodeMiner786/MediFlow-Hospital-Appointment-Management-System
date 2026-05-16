using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IRoleAssignmentLogRepository : IGenericRepository<RoleAssignmentLog>
    {
        // UserId অনুযায়ী স্ট্রিম
        IAsyncEnumerable<RoleAssignmentLog> GetLogsByTargetUserStream(Guid userId);

        // AssignedByUserId অনুযায়ী লিস্ট
        Task<IEnumerable<RoleAssignmentLog>> GetLogsByAdminAsync(Guid adminUserId);

        // ডেট রেঞ্জ
        Task<IEnumerable<RoleAssignmentLog>> GetLogsByDateRangeAsync(DateTime start, DateTime end);

        // ToRole অনুযায়ী কাউন্ট
        Task<int> GetCountByTargetRoleAsync(UserRole role);
    }
}