using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed;


namespace HealthcareHospitalManagement.Application.Validators.Feed;

public class FeedFilterRequestValidator : AbstractValidator<FeedFilterRequestDto>
{
    public FeedFilterRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City filter must not exceed 100 characters.")
            .When(x => x.City != null);

        RuleFor(x => x.Tag)
            .MaximumLength(50).WithMessage("Tag filter must not exceed 50 characters.")
            .When(x => x.Tag != null);
    }
}
