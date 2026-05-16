using HealthcareHospitalManagement.Application.Commands.Identity;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Application.Queries.Identity;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediFlow_API.Controllers.Identity
{
    [Authorize] // সব endpoint এ authorize enforced
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // ── Helper: JWT থেকে current user এর Id নাও ──────────────────────
        private Guid CurrentUserId
        {
            get
            {
                var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new UnauthorizedAccessException("User identity not found.");
                return Guid.Parse(userId);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ROLE MANAGEMENT  (Admin / SuperAdmin only)
        // ══════════════════════════════════════════════════════════════════

        [HttpPost("roles/assign")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> AssignRole(
            [FromBody] AssignRoleRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new AssignRoleCommand(dto, CurrentUserId),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("roles/revoke")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> RevokeRole(
            [FromBody] RevokeRoleRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new RevokeRoleCommand(dto.TargetUserId, CurrentUserId, dto.Reason),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ══════════════════════════════════════════════════════════════════
        // ACCOUNT MANAGEMENT  (Admin / SuperAdmin only)
        // ══════════════════════════════════════════════════════════════════

        [HttpPost("accounts/{userId:guid}/lock")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> LockAccount(
            [FromRoute] Guid userId,
            [FromBody] LockAccountRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new LockUserAccountCommand(userId, CurrentUserId, dto.Reason),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("accounts/{userId:guid}/unlock")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UnlockAccount(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new UnlockUserAccountCommand(userId, CurrentUserId),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ══════════════════════════════════════════════════════════════════
        // OTP
        // ══════════════════════════════════════════════════════════════════

        [HttpPost("otp/verify")]
        [Authorize]
        public async Task<IActionResult> VerifyOtp(
            [FromBody] VerifyOtpRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new VerifyOtpCommand(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ══════════════════════════════════════════════════════════════════
        // USER MANAGEMENT QUERIES  (Admin / SuperAdmin only)
        // ══════════════════════════════════════════════════════════════════

        [HttpGet("users")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] UserRole? role,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(
                new GetAllUsersQuery(role, pageNumber, pageSize),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("users/{userId:guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetUserById(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new GetUserByIdQuery(userId),
                cancellationToken);

            if (!response.Success && response.Message?.Contains("not found") == true)
                return NotFound(response);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("roles/logs")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetRoleAssignmentLogs(
            [FromQuery] Guid? targetUserId,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new GetRoleAssignmentLogsQuery(targetUserId),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("audit-logs")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAuditLogs(
            [FromQuery] AuditLogFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new GetAuditLogsQuery(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("users/{userId:guid}/login-history")]
        [Authorize(Roles = "Admin,SuperAdmin,Doctor,User")]
        public async Task<IActionResult> GetLoginHistory(
            [FromRoute] Guid userId,
            [FromQuery] int recentCount = 10,
            CancellationToken cancellationToken = default)
        {
            var isAdmin = User.IsInRole("Admin") || User.IsInRole("SuperAdmin");
            if (!isAdmin && CurrentUserId != userId)
                return Forbid();

            var response = await _mediator.Send(
                new GetUserLoginHistoryQuery(userId, recentCount),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
