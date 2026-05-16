using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class ForgotPasswordCommandHandler(IIdentityUnitOfWork uow, IPasswordHasher hasher)
    : IRequestHandler<ForgotPasswordCommand, bool>
{
    public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken ct)
    {
        var user = await uow.Users.GetByEmailAsync(request.Dto.Email);
        if (user == null) return false;

        var rawToken = Guid.NewGuid().ToString();
        var tokenHash = hasher.Hash(rawToken);

        await uow.PasswordResets.AddAsync(new PasswordResetRequest
        {
            UserId = user.Id,
            TokenHash = tokenHash,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            IsUsed = false
        }, ct); // ✅ ct যোগ

        await uow.AuditLogs.AddAsync(new AuditLog
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Action = AuditAction.ForgotPassword,
            EntityName = "PasswordResetRequest",
            IsSuccess = true,
            Timestamp = DateTime.UtcNow
        }, ct); // ✅ ct যোগ

        await uow.SaveChangesAsync(ct);

        // ৫. Email পাঠাবে পরে
        // await _emailService.SendResetEmailAsync(user.Email, rawToken);

        return true;
    }
}