using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Application.Queries.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using MediatR;

namespace MediFlow_API.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    [Tags("Auth")]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await sender.Send(new RegisterCommand(dto));
            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "User registered successfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await sender.Send(new LoginCommand(dto));
            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful"));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var result = await sender.Send(new RefreshTokenCommand(dto));
            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Token refreshed"));
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            var userId = Guid.Parse(userIdString);
            var result = await sender.Send(new GetUserProfileQuery(userId));

            return Ok(ApiResponse<UserProfileResponseDto>.SuccessResponse(result));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            await sender.Send(new ForgotPasswordCommand(dto));
            return Ok(ApiResponse<string>.SuccessResponse("Password reset link sent to email"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            await sender.Send(new ResetPasswordCommand(dto));
            return Ok(ApiResponse<string>.SuccessResponse("Password reset successful"));
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await sender.Send(new ChangePasswordCommand(userId, dto));
            return Ok(ApiResponse<string>.SuccessResponse("Password changed successfully"));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await sender.Send(new LogoutCommand(userId));
            return Ok(ApiResponse<string>.SuccessResponse("Logged out successfully"));
        }
    }
}