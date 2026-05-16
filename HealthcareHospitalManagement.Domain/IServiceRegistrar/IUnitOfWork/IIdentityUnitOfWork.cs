using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOtpRepository Otps { get; }
        IPasswordResetRepository PasswordResets { get; }
        IAuditLogRepository AuditLogs { get; }
        IRolePermissionRepository RolePermissions { get; }
        IRoleAssignmentLogRepository RoleAssignmentLogs { get; }
        IUserLoginHistoryRepository UserLoginHistories { get; }
        IUserNotificationRepository UserNotifications { get; }
        IUserPermissionOverrideRepository UserPermissionOverrides { get; }
        IUserRefreshTokenRepository UserRefreshTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
