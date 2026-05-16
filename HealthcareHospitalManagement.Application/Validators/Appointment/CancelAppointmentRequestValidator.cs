using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Appointment;


namespace HealthcareHospitalManagement.Application.Validators.Appointment;

public class CancelAppointmentRequestValidator : AbstractValidator<CancelAppointmentRequestDto>
{
    public CancelAppointmentRequestValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.")
            .NotEqual(Guid.Empty).WithMessage("AppointmentId must not be an empty GUID.");

        RuleFor(x => x.CancellationReason)
            .NotEmpty().WithMessage("Cancellation reason is required.")
            .Length(5, 500).WithMessage("Cancellation reason must be between 5 and 500 characters.");
    }
}
