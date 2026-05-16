using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IAuditLogRepository : IGenericRepository<AuditLog>
    {
        IAsyncEnumerable<AuditLog> GetByUserIdStream(Guid userId);

        Task<IEnumerable<AuditLog>> GetByEntityAsync(string entityName, string entityId, CancellationToken ct = default);

        Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);

        Task<IEnumerable<AuditLog>> GetFailedLogsAsync(CancellationToken ct = default);

        Task DeleteLogsOlderThanAsync(DateTime date, CancellationToken ct = default);

        Task<PagedResponse<AuditLog>> GetFilteredLogsAsync(
            Guid? userId,
            AuditAction? action,
            UserRole? role,
            DateTime? from,
            DateTime? to,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default);
    }
}
