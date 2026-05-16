using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Application.Queries.Auth;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Auth;

public class GetUserProfileQueryHandler(IIdentityUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetUserProfileQuery, UserProfileResponseDto>
{
    public async Task<UserProfileResponseDto> Handle(GetUserProfileQuery request, CancellationToken ct)
    {
        var user = await uow.Users.GetByIdAsync(request.UserId, ct)
               ?? throw new Exception("User not found!");


        var profileDto = mapper.Map<UserProfileResponseDto>(user);

        profileDto.AccountStatus = "Active";
        profileDto.IsEmailVerified = true;

        return profileDto;
    }
}
