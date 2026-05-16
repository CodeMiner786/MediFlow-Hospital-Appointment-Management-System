using HealthcareHospitalManagement.Application.DTOs.Auth;
using MediatR;

using System;

namespace HealthcareHospitalManagement.Application.Commands.Auth
{
    // Register
    public record RegisterCommand(RegisterRequestDto Dto) : IRequest<AuthResponseDto>;

    // Login
    public record LoginCommand(LoginRequestDto Dto) : IRequest<AuthResponseDto>;

    // Refresh Token
    public record RefreshTokenCommand(RefreshTokenRequestDto Dto) : IRequest<AuthResponseDto>;

    // Forgot Password
    public record ForgotPasswordCommand(ForgotPasswordRequestDto Dto) : IRequest<bool>;

    // Reset Password
    public record ResetPasswordCommand(ResetPasswordRequestDto Dto) : IRequest<bool>;

    // Change Password
    public record ChangePasswordCommand(Guid UserId, ChangePasswordRequestDto Dto) : IRequest<bool>;

    // Logout
    public record LogoutCommand(Guid UserId) : IRequest<bool>;
}
