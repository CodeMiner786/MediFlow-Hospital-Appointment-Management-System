using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;


namespace HealthcareHospitalManagement.Application.Validators.Pharmacy;

public class MedicineSearchRequestValidator : AbstractValidator<MedicineSearchRequestDto>
{
    public MedicineSearchRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.Name)
            .MaximumLength(200).WithMessage("Name filter must not exceed 200 characters.")
            .When(x => x.Name != null);

        RuleFor(x => x.GenericName)
            .MaximumLength(200).WithMessage("Generic name filter must not exceed 200 characters.")
            .When(x => x.GenericName != null);
    }
}
