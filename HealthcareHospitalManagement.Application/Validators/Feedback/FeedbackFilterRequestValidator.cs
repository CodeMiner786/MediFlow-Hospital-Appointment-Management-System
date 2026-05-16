using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feedback;


namespace HealthcareHospitalManagement.Application.Validators.Feedback;

public class FeedbackFilterRequestValidator : AbstractValidator<FeedbackFilterRequestDto>
{
    public FeedbackFilterRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.MinRating)
            .InclusiveBetween(1, 5).WithMessage("MinRating must be between 1 and 5.")
            .When(x => x.MinRating.HasValue);

        RuleFor(x => x.MaxRating)
            .InclusiveBetween(1, 5).WithMessage("MaxRating must be between 1 and 5.")
            .GreaterThanOrEqualTo(x => x.MinRating ?? 1).WithMessage("MaxRating must be greater than or equal to MinRating.")
            .When(x => x.MaxRating.HasValue);
    }
}
