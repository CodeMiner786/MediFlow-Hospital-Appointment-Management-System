using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Emergency;


namespace HealthcareHospitalManagement.Application.Validators.Emergency;

public class EmergencyVisitFilterRequestValidator : AbstractValidator<EmergencyVisitFilterRequestDto>
{
    public EmergencyVisitFilterRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.DateFrom)
            .LessThanOrEqualTo(x => x.DateTo).WithMessage("DateFrom must be before or equal to DateTo.")
            .When(x => x.DateFrom.HasValue && x.DateTo.HasValue);
    }
}
