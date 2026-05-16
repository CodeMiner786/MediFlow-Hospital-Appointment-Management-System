using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Patient;


namespace HealthcareHospitalManagement.Application.Validators.Patient;

public class UpdatePatientRequestValidator : AbstractValidator<UpdatePatientRequestDto>
{
    public UpdatePatientRequestValidator()
    {
        RuleFor(x => x.AlternatePhone)
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Alternate phone must be a valid phone number.")
            .When(x => x.AlternatePhone != null);

        RuleFor(x => x.Address)
            .MaximumLength(300).WithMessage("Address must not exceed 300 characters.")
            .When(x => x.Address != null);

        RuleFor(x => x.WeightKg)
            .InclusiveBetween(0.5m, 500).WithMessage("Weight must be between 0.5 and 500 kg.")
            .When(x => x.WeightKg.HasValue);

        RuleFor(x => x.HeightCm)
            .InclusiveBetween(20, 300).WithMessage("Height must be between 20 and 300 cm.")
            .When(x => x.HeightCm.HasValue);

        RuleFor(x => x.InsuranceProvider)
            .NotEmpty().WithMessage("Insurance provider is required when patient has insurance.")
            .When(x => x.HasInsurance == true);

        RuleFor(x => x.InsurancePolicyNumber)
            .NotEmpty().WithMessage("Insurance policy number is required when patient has insurance.")
            .When(x => x.HasInsurance == true);
    }
}
