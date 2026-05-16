using HealthcareHospitalManagement.Application.Commands.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Identity
{
    public class UnlockUserAccountCommandHandler(
        IIdentityUnitOfWork unitOfWork
    ) : IRequestHandler<UnlockUserAccountCommand, ApiResponse<bool>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<bool>> Handle(
            UnlockUserAccountCommand command,
            CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.GetByIdAsync(command.TargetUserId, cancellationToken);
            if (targetUser is null)
                return ApiResponse<bool>.FailResponse("Target user not found.");

            var adminUser = await _unitOfWork.Users.GetByIdAsync(command.AdminUserId, cancellationToken);
            if (adminUser is null)
                return ApiResponse<bool>.FailResponse("Admin user not found.");

            if (adminUser.Role is not (UserRole.Admin or UserRole.SuperAdmin))
                return ApiResponse<bool>.FailResponse("You do not have permission to unlock accounts.");

            if (!targetUser.IsLockedOut)
                return ApiResponse<bool>.FailResponse("User account is not locked.");

            await _unitOfWork.Users.UnlockUserAsync(targetUser.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true,
                $"Account '{targetUser.Email}' has been unlocked successfully.");
        }
    }
}
