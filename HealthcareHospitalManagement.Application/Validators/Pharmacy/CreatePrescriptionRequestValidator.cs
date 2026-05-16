using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;


namespace HealthcareHospitalManagement.Application.Validators.Pharmacy;

public class CreatePrescriptionRequestValidator : AbstractValidator<CreatePrescriptionRequestDto>
{
    public CreatePrescriptionRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required.")
            .NotEqual(Guid.Empty).WithMessage("DoctorId must not be an empty GUID.");

        RuleFor(x => x.Diagnosis)
            .NotEmpty().WithMessage("Diagnosis is required.")
            .MaximumLength(1000).WithMessage("Diagnosis must not exceed 1000 characters.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one prescription item is required.");

        RuleForEach(x => x.Items)
            .SetValidator(new PrescriptionItemRequestValidator());
    }
}
