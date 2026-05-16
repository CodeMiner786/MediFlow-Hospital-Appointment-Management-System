using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Lab;


namespace HealthcareHospitalManagement.Application.Validators.Lab;

public class CreateLabOrderRequestValidator : AbstractValidator<CreateLabOrderRequestDto>
{
    public CreateLabOrderRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.LabProfileId)
            .NotEmpty().WithMessage("LabProfileId is required.")
            .NotEqual(Guid.Empty).WithMessage("LabProfileId must not be an empty GUID.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid test priority.");

        RuleFor(x => x.ClinicalNotes)
            .MaximumLength(1000).WithMessage("Clinical notes must not exceed 1000 characters.")
            .When(x => x.ClinicalNotes != null);

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one lab order item is required.");

        RuleForEach(x => x.Items)
            .SetValidator(new LabOrderItemRequestValidator());
    }
}
