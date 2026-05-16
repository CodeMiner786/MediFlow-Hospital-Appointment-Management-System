using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Notification;

namespace HealthcareHospitalManagement.Application.Validators.Notification;

public class SendNotificationRequestValidator : AbstractValidator<SendNotificationRequestDto>
{
    public SendNotificationRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters.");

        RuleFor(x => x.NotificationType)
            .NotEmpty().WithMessage("Notification type is required.")
            .MaximumLength(50).WithMessage("Notification type must not exceed 50 characters.");
    }
}
