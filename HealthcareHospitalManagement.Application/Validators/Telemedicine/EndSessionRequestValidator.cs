using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;


namespace HealthcareHospitalManagement.Application.Validators.Telemedicine;

public class EndSessionRequestValidator : AbstractValidator<EndSessionRequestDto>
{
    public EndSessionRequestValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty().WithMessage("SessionId is required.")
            .NotEqual(Guid.Empty).WithMessage("SessionId must not be an empty GUID.");

        RuleFor(x => x.DoctorNotes)
            .MaximumLength(2000).WithMessage("Doctor notes must not exceed 2000 characters.")
            .When(x => x.DoctorNotes != null);

        RuleFor(x => x.Prescription)
            .MaximumLength(2000).WithMessage("Prescription must not exceed 2000 characters.")
            .When(x => x.Prescription != null);

        RuleFor(x => x.FollowUpInstructions)
            .MaximumLength(1000).WithMessage("Follow-up instructions must not exceed 1000 characters.")
            .When(x => x.FollowUpInstructions != null);

        RuleFor(x => x.FollowUpDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Follow-up date must be in the future.")
            .When(x => x.FollowUpDate.HasValue);
    }
}
