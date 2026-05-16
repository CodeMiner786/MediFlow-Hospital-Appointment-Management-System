using HealthcareHospitalManagement.Application.Commands.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Identity
{
    public class LockUserAccountCommandHandler(
        IIdentityUnitOfWork unitOfWork
    ) : IRequestHandler<LockUserAccountCommand, ApiResponse<bool>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<bool>> Handle(
            LockUserAccountCommand command,
            CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.GetByIdAsync(command.TargetUserId, cancellationToken);
            if (targetUser is null)
                return ApiResponse<bool>.FailResponse("Target user not found.");

            var adminUser = await _unitOfWork.Users.GetByIdAsync(command.AdminUserId, cancellationToken);
            if (adminUser is null)
                return ApiResponse<bool>.FailResponse("Admin user not found.");

            if (adminUser.Role is not (UserRole.Admin or UserRole.SuperAdmin))
                return ApiResponse<bool>.FailResponse("You do not have permission to lock accounts.");

            if (targetUser.IsLockedOut)
                return ApiResponse<bool>.FailResponse("User account is already locked.");

            // ৩০ দিনের জন্য lock করো (প্রয়োজনে পরিবর্তন করা যাবে)
            targetUser.LockoutEndAt = DateTime.UtcNow.AddDays(30);
            await _unitOfWork.Users.UpdateAsync(targetUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true,
                $"Account '{targetUser.Email}' locked until {targetUser.LockoutEndAt:dd MMM yyyy}.");
        }
    }
}
