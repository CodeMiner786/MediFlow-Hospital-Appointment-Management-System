using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Identity;



namespace HealthcareHospitalManagement.Application.Validators.Identity;

public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequestDto>
{
    public VerifyOtpRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");

        RuleFor(x => x.OtpCode)
            .NotEmpty().WithMessage("OTP code is required.")
            .Matches(@"^\d{4,8}$").WithMessage("OTP code must be 4 to 8 digits.");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("Purpose is required.")
            .MaximumLength(100).WithMessage("Purpose must not exceed 100 characters.");
    }
}
