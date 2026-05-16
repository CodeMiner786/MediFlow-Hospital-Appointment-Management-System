using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;

namespace HealthcareHospitalManagement.Application.Validators.Pharmacy;

public class CreateMedicineRequestValidator : AbstractValidator<CreateMedicineRequestDto>
{
    public CreateMedicineRequestValidator()
    {
        RuleFor(x => x.PharmacyProfileId)
            .NotEmpty().WithMessage("PharmacyProfileId is required.")
            .NotEqual(Guid.Empty).WithMessage("PharmacyProfileId must not be an empty GUID.");

        RuleFor(x => x.MedicineName)
            .NotEmpty().WithMessage("Medicine name is required.")
            .Length(2, 200).WithMessage("Medicine name must be between 2 and 200 characters.");

        RuleFor(x => x.GenericName)
            .NotEmpty().WithMessage("Generic name is required.")
            .MaximumLength(200).WithMessage("Generic name must not exceed 200 characters.");

        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("Brand name is required.")
            .MaximumLength(200).WithMessage("Brand name must not exceed 200 characters.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid medicine category.");

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .MaximumLength(200).WithMessage("Manufacturer must not exceed 200 characters.");

        RuleFor(x => x.Strength)
            .NotEmpty().WithMessage("Strength is required.")
            .MaximumLength(50).WithMessage("Strength must not exceed 50 characters.");

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit is required.")
            .MaximumLength(50).WithMessage("Unit must not exceed 50 characters.");

        RuleFor(x => x.SellingPrice)
            .GreaterThan(0).WithMessage("Selling price must be greater than 0.");
    }
}
