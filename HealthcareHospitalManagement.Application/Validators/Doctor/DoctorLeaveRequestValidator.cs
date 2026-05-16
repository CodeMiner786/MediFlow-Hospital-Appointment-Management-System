using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Doctor;


namespace HealthcareHospitalManagement.Application.Validators.Doctor;

public class DoctorLeaveRequestValidator : AbstractValidator<DoctorLeaveRequestDto>
{
    public DoctorLeaveRequestValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required.")
            .NotEqual(Guid.Empty).WithMessage("DoctorId must not be an empty GUID.");

        RuleFor(x => x.LeaveDate)
            .NotEmpty().WithMessage("Leave date is required.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Leave date cannot be in the past.");

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.")
            .When(x => x.Reason != null);
    }
}
