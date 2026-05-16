using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;

namespace HealthcareHospitalManagement.Application.Validators.Feed.Posts;

public class AddPostCommentRequestValidator : AbstractValidator<AddPostCommentRequestDto>
{
    public AddPostCommentRequestValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must not be an empty GUID.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MinimumLength(2).WithMessage("Content must be at least 2 characters long.")
            .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");

        RuleFor(x => x.ParentCommentId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("ParentCommentId must be null or a valid GUID.");
    }
}
