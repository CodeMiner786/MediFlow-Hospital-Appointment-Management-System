using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class RegisterCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IPasswordHasher hasher,
    IJwtTokenService jwt,
    IMapper mapper
) : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken ct)
    {
        var user = mapper.Map<ApplicationUser>(request.Dto);
        user.Id = Guid.NewGuid();
        user.PasswordHash = hasher.Hash(request.Dto.Password);

        await unitOfWork.Users.AddAsync(user, ct); // ✅ ct যোগ

        var response = mapper.Map<AuthResponseDto>(user);
        response.AccessToken = jwt.GenerateAccessToken(user);
        response.RefreshToken = jwt.GenerateRefreshToken();
        response.ExpiresAt = DateTime.UtcNow.AddHours(1);

        await unitOfWork.SaveChangesAsync(ct);

        return response;
    }
}