using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;


namespace HealthcareHospitalManagement.Application.Validators.Pharmacy;

public class MedicineOrderRequestValidator : AbstractValidator<MedicineOrderRequestDto>
{
    public MedicineOrderRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.PharmacyId)
            .NotEmpty().WithMessage("PharmacyId is required.")
            .NotEqual(Guid.Empty).WithMessage("PharmacyId must not be an empty GUID.");

        RuleFor(x => x.DeliveryAddress)
            .NotEmpty().WithMessage("Delivery address is required.")
            .MaximumLength(500).WithMessage("Delivery address must not exceed 500 characters.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one order item is required.");

        RuleForEach(x => x.Items)
            .SetValidator(new MedicineOrderItemRequestValidator());
    }
}
