using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;


namespace HealthcareHospitalManagement.Application.Validators.Pharmacy;

public class PrescriptionItemRequestValidator : AbstractValidator<PrescriptionItemRequestDto>
{
    public PrescriptionItemRequestValidator()
    {
        RuleFor(x => x.MedicineId)
            .NotEmpty().WithMessage("MedicineId is required.")
            .NotEqual(Guid.Empty).WithMessage("MedicineId must not be an empty GUID.");

        RuleFor(x => x.Dosage)
            .NotEmpty().WithMessage("Dosage is required.")
            .MaximumLength(100).WithMessage("Dosage must not exceed 100 characters.");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required.")
            .MaximumLength(100).WithMessage("Frequency must not exceed 100 characters.");

        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("Duration must be at least 1 day.")
            .LessThanOrEqualTo(365).WithMessage("Duration must not exceed 365 days.");
    }
}
