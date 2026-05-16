using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;


namespace HealthcareHospitalManagement.Application.Validators.Pharmacy;

public class MedicineOrderItemRequestValidator : AbstractValidator<MedicineOrderItemRequestDto>
{
    public MedicineOrderItemRequestValidator()
    {
        RuleFor(x => x.MedicineId)
            .NotEmpty().WithMessage("MedicineId is required.")
            .NotEqual(Guid.Empty).WithMessage("MedicineId must not be an empty GUID.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.");
    }
}
