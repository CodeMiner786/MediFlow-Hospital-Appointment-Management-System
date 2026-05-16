using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed.Posts
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequestDto>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId is required.")
                .NotEqual(Guid.Empty).WithMessage("PostId must not be an empty GUID.");

            RuleFor(x => x.Content)
                .MinimumLength(5).When(x => !string.IsNullOrEmpty(x.Content))
                .WithMessage("Content must be at least 5 characters long.")
                .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Content))
                .WithMessage("Content must not exceed 2000 characters.");

            RuleFor(x => x.Audience)
                .IsInEnum().When(x => x.Audience.HasValue)
                .WithMessage("Invalid Audience value.");

            RuleForEach(x => x.Tags)
                .MaximumLength(50).WithMessage("Each tag must not exceed 50 characters.");

            RuleForEach(x => x.MediaUrls)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Each MediaUrl must be a valid absolute URL.");
        }
    }
}
