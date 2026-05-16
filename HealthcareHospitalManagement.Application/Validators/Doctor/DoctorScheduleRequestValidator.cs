using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Doctor;

namespace HealthcareHospitalManagement.Application.Validators.Doctor;

public class DoctorScheduleRequestValidator : AbstractValidator<DoctorScheduleRequestDto>
{
    public DoctorScheduleRequestValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required.")
            .NotEqual(Guid.Empty).WithMessage("DoctorId must not be an empty GUID.");

        RuleFor(x => x.SlotDurationMinutes)
            .InclusiveBetween(5, 120).WithMessage("Slot duration must be between 5 and 120 minutes.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}
