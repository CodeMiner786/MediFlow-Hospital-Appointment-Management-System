using HealthcareHospitalManagement.Application.Commands.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Identity
{
    public class RevokeRoleCommandHandler(
        IIdentityUnitOfWork unitOfWork
    ) : IRequestHandler<RevokeRoleCommand, ApiResponse<bool>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<bool>> Handle(
            RevokeRoleCommand command,
            CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.GetByIdAsync(command.TargetUserId, cancellationToken);
            if (targetUser is null)
                return ApiResponse<bool>.FailResponse("Target user not found.");

            var adminUser = await _unitOfWork.Users.GetByIdAsync(command.AdminUserId, cancellationToken);
            if (adminUser is null)
                return ApiResponse<bool>.FailResponse("Admin user not found.");

            if (adminUser.Role is not (UserRole.Admin or UserRole.SuperAdmin))
                return ApiResponse<bool>.FailResponse("You do not have permission to revoke roles.");

            if (targetUser.Role == UserRole.User)
                return ApiResponse<bool>.FailResponse("User already has the base 'User' role.");

            var oldRole = targetUser.Role;

            targetUser.Role = UserRole.User;
            await _unitOfWork.Users.UpdateAsync(targetUser, cancellationToken);

            var log = new RoleAssignmentLog
            {
                UserId = targetUser.Id,
                FromRole = oldRole,
                ToRole = UserRole.User,
                AssignedByUserId = adminUser.Id,
                AssignedByEmail = adminUser.Email,
                AssignedByRole = adminUser.Role,
                AssignedAt = DateTime.UtcNow,
                Reason = command.Reason ?? "Role revoked by admin."
            };

            await _unitOfWork.RoleAssignmentLogs.AddAsync(log, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true,
                $"Role revoked from '{targetUser.Email}'. Now assigned: User.");
        }
    }
}
