using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Payment;

namespace HealthcareHospitalManagement.Application.Validators.Payment;

public class RefundRequestValidator : AbstractValidator<RefundRequestDto>
{
    public RefundRequestValidator()
    {
        RuleFor(x => x.PaymentTransactionId)
            .NotEmpty().WithMessage("PaymentTransactionId is required.")
            .NotEqual(Guid.Empty).WithMessage("PaymentTransactionId must not be an empty GUID.");

        RuleFor(x => x.RefundReason)
            .NotEmpty().WithMessage("Refund reason is required.")
            .Length(5, 500).WithMessage("Refund reason must be between 5 and 500 characters.");
    }
}
