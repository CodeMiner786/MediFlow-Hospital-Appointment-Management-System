using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Billing;


namespace HealthcareHospitalManagement.Application.Validators.Billing;

public class BillItemRequestValidator : AbstractValidator<BillItemRequestDto>
{
    public BillItemRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(300).WithMessage("Description must not exceed 300 characters.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.");
    }
}
