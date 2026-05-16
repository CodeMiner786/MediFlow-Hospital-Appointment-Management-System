using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Chat;


namespace HealthcareHospitalManagement.Application.Validators.Chat;

public class SendMessageRequestValidator : AbstractValidator<SendMessageRequestDto>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty().WithMessage("ConversationId is required.")
            .NotEqual(Guid.Empty).WithMessage("ConversationId must not be an empty GUID.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("SenderId is required.")
            .NotEqual(Guid.Empty).WithMessage("SenderId must not be an empty GUID.");

        RuleFor(x => x.SenderType)
            .IsInEnum().WithMessage("Invalid sender type.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(4000).WithMessage("Message content must not exceed 4000 characters.");

        RuleFor(x => x.AttachmentSizeBytes)
            .GreaterThan(0).WithMessage("Attachment size must be greater than 0.")
            .When(x => x.AttachmentSizeBytes.HasValue);
    }
}
