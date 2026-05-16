using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Chat;


namespace HealthcareHospitalManagement.Application.Validators.Chat;

public class MessageFilterRequestValidator : AbstractValidator<MessageFilterRequestDto>
{
    public MessageFilterRequestValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty().WithMessage("ConversationId is required.")
            .NotEqual(Guid.Empty).WithMessage("ConversationId must not be an empty GUID.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
    }
}
