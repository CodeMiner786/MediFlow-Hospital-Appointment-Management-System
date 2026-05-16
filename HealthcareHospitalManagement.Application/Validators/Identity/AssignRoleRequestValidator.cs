using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Identity;


namespace HealthcareHospitalManagement.Application.Validators.Identity;

public class AssignRoleRequestValidator : AbstractValidator<AssignRoleRequestDto>
{
    public AssignRoleRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");

        RuleFor(x => x.NewRole)
            .NotEmpty().WithMessage("New role is required.")
            .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required.")
            .Length(5, 500).WithMessage("Reason must be between 5 and 500 characters.");
    }
}
