using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Appointment;


namespace HealthcareHospitalManagement.Application.Validators.Appointment;

public class RescheduleAppointmentRequestValidator : AbstractValidator<RescheduleAppointmentRequestDto>
{
    public RescheduleAppointmentRequestValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.")
            .NotEqual(Guid.Empty).WithMessage("AppointmentId must not be an empty GUID.");

        RuleFor(x => x.NewDate)
            .NotEmpty().WithMessage("NewDate is required.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("NewDate cannot be in the past.");
    }
}
