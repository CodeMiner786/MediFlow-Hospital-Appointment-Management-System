using HealthcareHospitalManagement.Application.Commands.Auth;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Auth;

public class LogoutCommandHandler(IIdentityUnitOfWork uow)
    : IRequestHandler<LogoutCommand, bool>
{
    public async Task<bool> Handle(LogoutCommand request, CancellationToken ct)
    {
        // ১. User এর সব refresh token বাতিল করো
        var tokens = await uow.UserRefreshTokens
            .FindAsync(t => t.UserId == request.UserId && !t.IsRevoked, ct);   

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
            await uow.UserRefreshTokens.UpdateAsync(token, ct);     
        }

        // ২. AuditLog এ logout রাখো
        await uow.AuditLogs.AddAsync(new()
        {
            UserId = request.UserId,
            Action = AuditAction.Logout,
            CreatedAt = DateTime.UtcNow
        }, ct);

        await uow.SaveChangesAsync(ct);
        return true;
    }
}
