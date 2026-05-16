using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class ResetPasswordCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IPasswordHasher hasher
) : IRequestHandler<ResetPasswordCommand, bool>
{
    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken ct)
    {
        // ১. ইউজার খুঁজে বের করা
        var user = await unitOfWork.Users.GetByEmailAsync(request.Dto.Email);
        if (user is null) return false;

        // ২. নতুন পাসওয়ার্ড হ্যাশ করা
        user.PasswordHash = hasher.Hash(request.Dto.NewPassword);

        // ৩. ইউজার আপডেট করা
        await unitOfWork.Users.UpdateAsync(user, ct);

        // ৪. SaveChangesAsync কল করা
        await unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}
