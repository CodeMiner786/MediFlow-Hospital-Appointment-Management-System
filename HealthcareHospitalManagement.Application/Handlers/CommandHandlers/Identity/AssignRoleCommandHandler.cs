using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Identity;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Identity
{
    public class AssignRoleCommandHandler(
        IIdentityUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<AssignRoleCommand, ApiResponse<RoleAssignmentLogResponseDto>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<RoleAssignmentLogResponseDto>> Handle(
            AssignRoleCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;

            var targetUser = await _unitOfWork.Users.GetByIdAsync(dto.UserId, cancellationToken);
            if (targetUser is null)
                return ApiResponse<RoleAssignmentLogResponseDto>.FailResponse("Target user not found.");

            var adminUser = await _unitOfWork.Users.GetByIdAsync(command.AssignedByUserId, cancellationToken);
            if (adminUser is null)
                return ApiResponse<RoleAssignmentLogResponseDto>.FailResponse("Admin user not found.");

            if (adminUser.Role is not (UserRole.Admin or UserRole.SuperAdmin))
                return ApiResponse<RoleAssignmentLogResponseDto>.FailResponse("You do not have permission to assign roles.");

            if (!Enum.TryParse<UserRole>(dto.NewRole, ignoreCase: true, out var newRole))
                return ApiResponse<RoleAssignmentLogResponseDto>.FailResponse(
                    $"Invalid role: '{dto.NewRole}'. Valid roles: {string.Join(", ", Enum.GetNames<UserRole>())}");

            if (targetUser.Role == newRole)
                return ApiResponse<RoleAssignmentLogResponseDto>.FailResponse($"User already has the role '{newRole}'.");

            var oldRole = targetUser.Role;

            targetUser.Role = newRole;
            await _unitOfWork.Users.UpdateAsync(targetUser, cancellationToken);

            var log = new RoleAssignmentLog
            {
                UserId = targetUser.Id,
                FromRole = oldRole,
                ToRole = newRole,
                AssignedByUserId = adminUser.Id,
                AssignedByEmail = adminUser.Email,
                AssignedByRole = adminUser.Role,
                AssignedAt = DateTime.UtcNow,
                Reason = dto.Reason
            };

            await _unitOfWork.RoleAssignmentLogs.AddAsync(log, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseDto = new RoleAssignmentLogResponseDto
            {
                Id = log.Id,
                UserName = $"{targetUser.FirstName} {targetUser.LastName}",
                OldRole = oldRole.ToString(),
                NewRole = newRole.ToString(),
                AssignedByName = $"{adminUser.FirstName} {adminUser.LastName}",
                Reason = dto.Reason ?? string.Empty,
                AssignedAt = log.AssignedAt
            };

            return ApiResponse<RoleAssignmentLogResponseDto>.SuccessResponse(
                responseDto,
                $"Role '{newRole}' assigned to '{targetUser.Email}' successfully.");
        }
    }
}
