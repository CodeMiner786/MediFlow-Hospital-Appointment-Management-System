using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Patient;

namespace HealthcareHospitalManagement.Application.Validators.Patient;

public class PatientSearchRequestValidator : AbstractValidator<PatientSearchRequestDto>
{
    public PatientSearchRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.FirstName)
            .MaximumLength(100).WithMessage("FirstName must not exceed 100 characters.")
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .MaximumLength(100).WithMessage("LastName must not exceed 100 characters.")
            .When(x => x.LastName != null);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 20 characters.")
            .When(x => x.PhoneNumber != null);

        RuleFor(x => x.PatientCode)
            .MaximumLength(50).WithMessage("PatientCode must not exceed 50 characters.")
            .When(x => x.PatientCode != null);
    }
}