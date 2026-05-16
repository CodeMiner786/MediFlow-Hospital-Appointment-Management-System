//using HealthcareHospitalManagement.Application.Commands.Identity;
//using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
//using MediatR;

//namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Identity;

//public class VerifyOtpCommandHandler(IIdentityUnitOfWork uow)
//    : IRequestHandler<VerifyOtpCommand, bool>
//{
//    public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken ct)
//    {
//        var otp = await uow.Otps.FirstOrDefaultAsync(x =>
//            x.UserId == request.Dto.UserId &&
//            x.Code == request.Dto.OtpCode, ct);   // 🔹 ct যোগ করা হয়েছে

//        if (otp is null || otp.IsUsed || otp.ExpiresAt < DateTime.UtcNow)
//            return false;

//        otp.IsUsed = true;
//        await uow.Otps.UpdateAsync(otp, ct);      // 🔹 ct যোগ করা হয়েছে
//        await uow.SaveChangesAsync(ct);

//        return true;
//    }
//}
