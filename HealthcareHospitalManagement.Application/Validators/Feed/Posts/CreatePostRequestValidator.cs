using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;


namespace HealthcareHospitalManagement.Application.Validators.Feed.Posts;


    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequestDto>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.OwnerUserId)
                .NotEmpty().WithMessage("OwnerUserId is required.")
                .NotEqual(Guid.Empty).WithMessage("OwnerUserId must not be an empty GUID.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(5).WithMessage("Content must be at least 5 characters long.")
                .MaximumLength(2000).WithMessage("Content must not exceed 2000 characters.");

            RuleFor(x => x.PostType)
                .IsInEnum().WithMessage("Invalid PostType value.");

            RuleFor(x => x.Audience)
                .IsInEnum().WithMessage("Invalid Audience value.");

            RuleFor(x => x.TargetCity)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.TargetCity));

            RuleFor(x => x.TargetSpecialty)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.TargetSpecialty));

            RuleForEach(x => x.MediaUrls)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Each MediaUrl must be a valid absolute URL.");

            RuleForEach(x => x.Tags)
                .MaximumLength(50).WithMessage("Each tag must not exceed 50 characters.");

            RuleFor(x => x.Language)
                .MaximumLength(10).When(x => !string.IsNullOrEmpty(x.Language));
        }
    }


