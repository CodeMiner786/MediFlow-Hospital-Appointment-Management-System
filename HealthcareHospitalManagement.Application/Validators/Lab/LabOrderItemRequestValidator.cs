using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Lab;


namespace HealthcareHospitalManagement.Application.Validators.Lab;

public class LabOrderItemRequestValidator : AbstractValidator<LabOrderItemRequestDto>
{
    public LabOrderItemRequestValidator()
    {
        RuleFor(x => x.LabTestId)
            .NotEmpty().WithMessage("LabTestId is required.")
            .NotEqual(Guid.Empty).WithMessage("LabTestId must not be an empty GUID.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes != null);
    }
}
