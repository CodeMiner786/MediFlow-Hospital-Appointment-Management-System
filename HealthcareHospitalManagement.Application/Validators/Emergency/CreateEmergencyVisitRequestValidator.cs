using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Emergency;

namespace HealthcareHospitalManagement.Application.Validators.Emergency;

public class CreateEmergencyVisitRequestValidator : AbstractValidator<CreateEmergencyVisitRequestDto>
{
    public CreateEmergencyVisitRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.TriageLevel)
            .IsInEnum().WithMessage("Invalid triage level.");

        RuleFor(x => x.ChiefComplaint)
            .NotEmpty().WithMessage("Chief complaint is required.")
            .Length(5, 1000).WithMessage("Chief complaint must be between 5 and 1000 characters.");

        RuleFor(x => x.ModeOfArrival)
            .MaximumLength(100).WithMessage("Mode of arrival must not exceed 100 characters.")
            .When(x => x.ModeOfArrival != null);

        RuleFor(x => x.InitialAssessment)
            .MaximumLength(2000).WithMessage("Initial assessment must not exceed 2000 characters.")
            .When(x => x.InitialAssessment != null);
    }
}
