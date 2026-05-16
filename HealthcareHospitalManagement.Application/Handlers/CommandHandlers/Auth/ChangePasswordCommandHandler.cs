using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class ChangePasswordCommandHandler(IIdentityUnitOfWork uow, IPasswordHasher hasher)
    : IRequestHandler<ChangePasswordCommand, bool>
{
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var user = await uow.Users.GetByIdAsync(request.UserId, ct);
        if (user == null) return false;

        if (!hasher.Verify(request.Dto.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("পুরনো password সঠিক নয়!");

        user.PasswordHash = hasher.Hash(request.Dto.NewPassword);
        await uow.Users.UpdateAsync(user, ct);

        await uow.AuditLogs.AddAsync(new AuditLog
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Action = AuditAction.ChangePassword,
            EntityName = "ApplicationUser",
            IsSuccess = true,
            Timestamp = DateTime.UtcNow
        }, ct); // ✅ ct যোগ

        await uow.SaveChangesAsync(ct);
        return true;
    }
}