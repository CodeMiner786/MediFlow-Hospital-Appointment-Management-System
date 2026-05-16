using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed
{
    public class CreateFeedPostRequestValidator : AbstractValidator<CreateFeedPostRequestDto>
    {
        public CreateFeedPostRequestValidator()
        {
            RuleFor(x => x.OwnerUserId)
                .NotEmpty().WithMessage("OwnerUserId is required.")
                .NotEqual(Guid.Empty).WithMessage("OwnerUserId must not be an empty GUID.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(5).WithMessage("Content must be at least 5 characters long.")
                .MaximumLength(2000).WithMessage("Content must not exceed 2000 characters.");

            RuleFor(x => x.Audience)
                .IsInEnum().WithMessage("Invalid Audience value.");

            RuleForEach(x => x.MediaUrls)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Each MediaUrl must be a valid absolute URL.");

            RuleForEach(x => x.Tags)
                .MaximumLength(50).WithMessage("Each tag must not exceed 50 characters.");
        }
    }
}
