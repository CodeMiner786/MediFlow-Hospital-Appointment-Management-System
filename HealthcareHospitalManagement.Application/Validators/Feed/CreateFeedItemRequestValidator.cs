using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed;


namespace HealthcareHospitalManagement.Application.Validators.Feed;

public class CreateFeedItemRequestValidator : AbstractValidator<CreateFeedItemRequestDto>
{
    public CreateFeedItemRequestValidator()
    {
        RuleFor(x => x.OwnerUserId)
            .NotEmpty().WithMessage("OwnerUserId is required.")
            .NotEqual(Guid.Empty).WithMessage("OwnerUserId must not be an empty GUID.");

        RuleFor(x => x.OwnerProfileId)
            .NotEmpty().WithMessage("OwnerProfileId is required.")
            .NotEqual(Guid.Empty).WithMessage("OwnerProfileId must not be an empty GUID.");

        RuleFor(x => x.ItemType)
            .IsInEnum().WithMessage("Invalid feed item type.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 200).WithMessage("Title must be between 3 and 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.")
            .When(x => x.Description != null);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.")
            .When(x => x.Latitude.HasValue);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.")
            .When(x => x.Longitude.HasValue);

        RuleFor(x => x.PrimaryAction)
            .IsInEnum().WithMessage("Invalid primary action type.");
    }
}
