using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;

namespace HealthcareHospitalManagement.Application.Validators.Telemedicine;

public class CreateTelemedicineSessionRequestValidator : AbstractValidator<CreateTelemedicineSessionRequestDto>
{
    public CreateTelemedicineSessionRequestValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.")
            .NotEqual(Guid.Empty).WithMessage("AppointmentId must not be an empty GUID.");

        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required.")
            .NotEqual(Guid.Empty).WithMessage("DoctorId must not be an empty GUID.");

        RuleFor(x => x.Provider)
            .IsInEnum().WithMessage("Invalid video call provider.");

        RuleFor(x => x.ScheduledAt)
            .NotEmpty().WithMessage("ScheduledAt is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("ScheduledAt must be in the future.");
    }
}
