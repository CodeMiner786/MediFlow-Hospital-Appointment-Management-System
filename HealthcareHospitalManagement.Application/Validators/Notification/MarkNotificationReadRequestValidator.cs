using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Notification;

namespace HealthcareHospitalManagement.Application.Validators.Notification;

public class MarkNotificationReadRequestValidator : AbstractValidator<MarkNotificationReadRequestDto>
{
    public MarkNotificationReadRequestValidator()
    {
        RuleFor(x => x.NotificationId)
            .NotEmpty().WithMessage("NotificationId is required.")
            .NotEqual(Guid.Empty).WithMessage("NotificationId must not be an empty GUID.");
    }
}
