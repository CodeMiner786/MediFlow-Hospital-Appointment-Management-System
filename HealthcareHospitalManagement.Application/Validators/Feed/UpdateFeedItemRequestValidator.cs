using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed
{
    public class UpdateFeedItemRequestValidator : AbstractValidator<UpdateFeedItemRequestDto>
    {
        public UpdateFeedItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("FeedItem Id is required.")
                .NotEqual(Guid.Empty).WithMessage("FeedItem Id must not be an empty GUID.");

            RuleFor(x => x.Title)
                .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.Title))
                .WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Title))
                .WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.SubTitle)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.SubTitle));

            RuleFor(x => x.Description)
                .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ImageUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ImageUrl must be a valid absolute URL.");

            RuleFor(x => x.CoverImageUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("CoverImageUrl must be a valid absolute URL.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue)
                .WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue)
                .WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.City)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.City));

            RuleFor(x => x.PrimaryAction)
                .IsInEnum().When(x => x.PrimaryAction.HasValue)
                .WithMessage("Invalid PrimaryAction value.");

            RuleForEach(x => x.Tags)
                .MaximumLength(50).WithMessage("Each tag must not exceed 50 characters.");
        }
    }
}
