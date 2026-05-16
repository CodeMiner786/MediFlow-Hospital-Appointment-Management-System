using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Wards;


namespace HealthcareHospitalManagement.Application.Validators.Wards;

public class BedFilterRequestValidator : AbstractValidator<BedFilterRequestDto>
{
    public BedFilterRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.WardId)
            .NotEqual(Guid.Empty).WithMessage("WardId must not be an empty GUID.")
            .When(x => x.WardId.HasValue);
    }
}
