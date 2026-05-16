using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Payment;

namespace HealthcareHospitalManagement.Application.Validators.Payment;

public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequestDto>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty().WithMessage("BillId is required.")
            .NotEqual(Guid.Empty).WithMessage("BillId must not be an empty GUID.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Payment amount must be greater than 0.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");

        RuleFor(x => x.ReceivedBy)
            .NotEmpty().WithMessage("ReceivedBy is required.")
            .MaximumLength(100).WithMessage("ReceivedBy must not exceed 100 characters.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500).WithMessage("Remarks must not exceed 500 characters.")
            .When(x => x.Remarks != null);
    }
}
