using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Notification;

namespace HealthcareHospitalManagement.Application.Validators.Notification;

public class NotificationFilterRequestValidator : AbstractValidator<NotificationFilterRequestDto>
{
    public NotificationFilterRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
    }
}
