using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Identity;
using HealthcareHospitalManagement.Infrastructure.Repositories.Notification;


namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class IdentityUnitOfWork(ApplicationDbContext context) : IIdentityUnitOfWork
    {
        public IUserRepository Users { get; } = new UserRepository(context);
        public IOtpRepository Otps { get; } = new OtpRepository(context);
        public IPasswordResetRepository PasswordResets { get; } = new PasswordResetRepository(context);
        public IAuditLogRepository AuditLogs { get; } = new AuditLogRepository(context);
        public IRolePermissionRepository RolePermissions { get; } = new RolePermissionRepository(context);
        public IRoleAssignmentLogRepository RoleAssignmentLogs { get; } = new RoleAssignmentLogRepository(context);
        public IUserLoginHistoryRepository UserLoginHistories { get; } = new UserLoginHistoryRepository(context);
        public IUserNotificationRepository UserNotifications { get; } = new UserNotificationRepository(context);
        public IUserPermissionOverrideRepository UserPermissionOverrides { get; } = new UserPermissionOverrideRepository(context);
        public IUserRefreshTokenRepository UserRefreshTokens { get; } = new UserRefreshTokenRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
