using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Chat;


namespace HealthcareHospitalManagement.Application.Validators.Chat;

public class StartConversationRequestValidator : AbstractValidator<StartConversationRequestDto>
{
    public StartConversationRequestValidator()
    {
        RuleFor(x => x.InitiatorUserId)
            .NotEmpty().WithMessage("InitiatorUserId is required.")
            .NotEqual(Guid.Empty).WithMessage("InitiatorUserId must not be an empty GUID.");

        RuleFor(x => x.RecipientUserId)
            .NotEmpty().WithMessage("RecipientUserId is required.")
            .NotEqual(Guid.Empty).WithMessage("RecipientUserId must not be an empty GUID.")
            .NotEqual(x => x.InitiatorUserId).WithMessage("RecipientUserId must differ from InitiatorUserId.");

        RuleFor(x => x.ConversationType)
            .IsInEnum().WithMessage("Invalid conversation type.");

        RuleFor(x => x.InitialMessage)
            .MaximumLength(4000).WithMessage("Initial message must not exceed 4000 characters.")
            .When(x => x.InitialMessage != null);
    }
}
