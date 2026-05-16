using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Billing;


namespace HealthcareHospitalManagement.Application.Validators.Billing;

public class UpdatePaymentStatusRequestValidator : AbstractValidator<UpdatePaymentStatusRequestDto>
{
    public UpdatePaymentStatusRequestValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty().WithMessage("BillId is required.")
            .NotEqual(Guid.Empty).WithMessage("BillId must not be an empty GUID.");

        RuleFor(x => x.AmountPaid)
            .GreaterThan(0).WithMessage("Amount paid must be greater than 0.");

        RuleFor(x => x.PaymentStatus)
            .IsInEnum().WithMessage("Invalid payment status.");
    }
}
