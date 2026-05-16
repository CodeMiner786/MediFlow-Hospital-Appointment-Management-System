using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feedback;

namespace HealthcareHospitalManagement.Application.Validators.Feedback;

public class CreateFeedbackRequestValidator : AbstractValidator<CreateFeedbackRequestDto>
{
    public CreateFeedbackRequestValidator()
    {
        RuleFor(x => x.ReviewerUserId)
            .NotEmpty().WithMessage("ReviewerUserId is required.")
            .NotEqual(Guid.Empty).WithMessage("ReviewerUserId must not be an empty GUID.");

        RuleFor(x => x.TargetId)
            .NotEmpty().WithMessage("TargetId is required.")
            .NotEqual(Guid.Empty).WithMessage("TargetId must not be an empty GUID.");

        RuleFor(x => x.TargetType)
            .NotEmpty().WithMessage("TargetType is required.")
            .MaximumLength(50).WithMessage("TargetType must not exceed 50 characters.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage("Comment must not exceed 1000 characters.")
            .When(x => x.Comment != null);
    }
}
