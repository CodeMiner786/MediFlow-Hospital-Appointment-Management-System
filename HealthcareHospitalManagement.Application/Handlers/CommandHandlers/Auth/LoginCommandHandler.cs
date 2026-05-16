using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.Enums.LoginWith;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class LoginCommandHandler(
    IIdentityUnitOfWork uow,
    IPasswordHasher hasher,
    IJwtTokenService jwt,
    IMapper mapper)
    : IRequestHandler<LoginCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await uow.Users.GetByEmailAsync(request.Dto.Email);

        if (user == null || !hasher.Verify(request.Dto.Password, user.PasswordHash))
        {
            if (user != null)
            {
                await uow.UserLoginHistories.AddAsync(new UserLoginHistory
                {
                    UserId = user.Id,
                    LoginAt = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = "Invalid password",
                    LoginProvider = LoginProvider.Local
                }, ct); // ✅ ct যোগ
                await uow.SaveChangesAsync(ct);
            }
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var accessToken = jwt.GenerateAccessToken(user);
        var refreshToken = jwt.GenerateRefreshToken();

        await uow.UserRefreshTokens.AddAsync(new UserRefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            JwtId = Guid.NewGuid().ToString()
        }, ct); // ✅ ct যোগ

        await uow.UserLoginHistories.AddAsync(new UserLoginHistory
        {
            UserId = user.Id,
            LoginAt = DateTime.UtcNow,
            IsSuccess = true,
            LoginProvider = LoginProvider.Local
        }, ct); // ✅ ct যোগ

        await uow.AuditLogs.AddAsync(new AuditLog
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Action = AuditAction.Login,
            EntityName = "ApplicationUser",
            IsSuccess = true,
            Timestamp = DateTime.UtcNow
        }, ct); // ✅ ct যোগ

        await uow.SaveChangesAsync(ct);

        var response = mapper.Map<AuthResponseDto>(user);
        response.AccessToken = accessToken;
        response.RefreshToken = refreshToken;
        response.ExpiresAt = DateTime.UtcNow.AddHours(1);

        return response;
    }
}