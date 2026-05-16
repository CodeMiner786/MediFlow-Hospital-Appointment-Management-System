using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Doctor;


namespace HealthcareHospitalManagement.Application.Validators.Doctor;

public class DoctorSearchRequestValidator : AbstractValidator<DoctorSearchRequestDto>
{
    public DoctorSearchRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name filter must not exceed 100 characters.")
            .When(x => x.Name != null);
    }
}
