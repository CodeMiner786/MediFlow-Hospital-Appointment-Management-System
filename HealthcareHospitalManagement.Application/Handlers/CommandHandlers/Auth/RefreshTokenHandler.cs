using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class RefreshTokenHandler(
    IIdentityUnitOfWork unitOfWork,
    IJwtTokenService jwt,
    IMapper mapper
) : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        // RefreshToken repository access via UnitOfWork
        var storedToken = await unitOfWork.UserRefreshTokens.GetByTokenAsync(request.Dto.RefreshToken)
                          ?? throw new Exception("Invalid Refresh Token");

        // User repository access via UnitOfWork
        var user = await unitOfWork.Users.GetByIdAsync(storedToken.UserId, ct)
                   ?? throw new Exception("User not found");

        var response = mapper.Map<AuthResponseDto>(user);

        response.AccessToken = jwt.GenerateAccessToken(user);
        response.RefreshToken = jwt.GenerateRefreshToken();
        response.ExpiresAt = DateTime.UtcNow.AddHours(1);

        // Save changes if needed (e.g. logging, token updates)
        await unitOfWork.SaveChangesAsync(ct);

        return response;
    }
}
