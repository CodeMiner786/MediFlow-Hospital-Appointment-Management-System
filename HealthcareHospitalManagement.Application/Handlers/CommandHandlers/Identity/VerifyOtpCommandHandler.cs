using HealthcareHospitalManagement.Application.Commands.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Enums.OTP;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Identity
{
    public class VerifyOtpCommandHandler(
        IIdentityUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher
    ) : IRequestHandler<VerifyOtpCommand, ApiResponse<bool>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ApiResponse<bool>> Handle(
            VerifyOtpCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;

            if (!Enum.TryParse<OtpPurpose>(dto.Purpose, ignoreCase: true, out var purpose))
                return ApiResponse<bool>.FailResponse($"Invalid OTP purpose: '{dto.Purpose}'.");

            var otp = await _unitOfWork.Otps.GetLatestValidOtpAsync(dto.UserId, purpose);
            if (otp is null)
                return ApiResponse<bool>.FailResponse("No valid OTP found. Please request a new one.");

            if (otp.IsBlocked)
                return ApiResponse<bool>.FailResponse("OTP is blocked due to too many failed attempts.");

            if (otp.IsExpired)
                return ApiResponse<bool>.FailResponse("OTP has expired. Please request a new one.");

            var isValid = _passwordHasher.Verify(dto.OtpCode, otp.CodeHash);
            if (!isValid)
            {
                await _unitOfWork.Otps.IncrementAttemptAsync(otp.Id);
                return ApiResponse<bool>.FailResponse("Invalid OTP code.");
            }

            await _unitOfWork.Otps.MarkAsUsedAsync(otp.Id);

            var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId, cancellationToken);
            if (user is not null)
            {
                if (purpose == OtpPurpose.EmailVerification)
                {
                    user.IsEmailVerified = true;
                    user.EmailVerifiedAt = DateTime.UtcNow;
                }
                else if (purpose == OtpPurpose.PhoneVerification)
                {
                    user.IsPhoneVerified = true;
                    user.PhoneVerifiedAt = DateTime.UtcNow;
                }
                await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true, "OTP verified successfully.");
        }
    }
}
