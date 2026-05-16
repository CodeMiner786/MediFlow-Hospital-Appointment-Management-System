using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Billing;


namespace HealthcareHospitalManagement.Application.Validators.Billing;

public class CreateBillRequestValidator : AbstractValidator<CreateBillRequestDto>
{
    public CreateBillRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.BillType)
            .IsInEnum().WithMessage("Invalid bill type.");

        RuleFor(x => x.DiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be 0 or greater.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one bill item is required.");

        RuleForEach(x => x.Items)
            .SetValidator(new BillItemRequestValidator());
    }
}
