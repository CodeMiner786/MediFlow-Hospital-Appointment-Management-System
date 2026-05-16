using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Appointment;


namespace HealthcareHospitalManagement.Application.Validators.Appointment;

public class BookAppointmentRequestValidator : AbstractValidator<BookAppointmentRequestDto>
{
    public BookAppointmentRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required.")
            .NotEqual(Guid.Empty).WithMessage("DoctorId must not be an empty GUID.");

        RuleFor(x => x.AppointmentDate)
            .NotEmpty().WithMessage("AppointmentDate is required.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("AppointmentDate cannot be in the past.");

        RuleFor(x => x.AppointmentType)
            .IsInEnum().WithMessage("Invalid appointment type.");

        RuleFor(x => x.ReasonForVisit)
            .NotEmpty().WithMessage("Reason for visit is required.")
            .Length(5, 500).WithMessage("Reason for visit must be between 5 and 500 characters.");

        RuleFor(x => x.Symptoms)
            .MaximumLength(1000).WithMessage("Symptoms must not exceed 1000 characters.")
            .When(x => x.Symptoms != null);

        RuleFor(x => x.PreviousAppointmentId)
            .NotEqual(Guid.Empty).WithMessage("PreviousAppointmentId must not be an empty GUID.")
            .When(x => x.PreviousAppointmentId.HasValue);
    }
}
