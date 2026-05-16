using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Patient;


namespace HealthcareHospitalManagement.Application.Validators.Patient;

public class PatientVitalRequestValidator : AbstractValidator<PatientVitalRequestDto>
{
    public PatientVitalRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.BloodPressureSystolic)
            .InclusiveBetween(50, 300).WithMessage("Systolic blood pressure must be between 50 and 300 mmHg.")
            .When(x => x.BloodPressureSystolic.HasValue);

        RuleFor(x => x.BloodPressureDiastolic)
            .InclusiveBetween(30, 200).WithMessage("Diastolic blood pressure must be between 30 and 200 mmHg.")
            .When(x => x.BloodPressureDiastolic.HasValue);

        RuleFor(x => x.PulseRate)
            .InclusiveBetween(20, 300).WithMessage("Pulse rate must be between 20 and 300 bpm.")
            .When(x => x.PulseRate.HasValue);

        RuleFor(x => x.Temperature)
            .InclusiveBetween(30, 45).WithMessage("Temperature must be between 30 and 45 °C.")
            .When(x => x.Temperature.HasValue);

        RuleFor(x => x.OxygenSaturation)
            .InclusiveBetween(0, 100).WithMessage("Oxygen saturation must be between 0 and 100%.")
            .When(x => x.OxygenSaturation.HasValue);

        RuleFor(x => x.WeightKg)
            .InclusiveBetween(0.5m, 500).WithMessage("Weight must be between 0.5 and 500 kg.")
            .When(x => x.WeightKg.HasValue);

        RuleFor(x => x.HeightCm)
            .InclusiveBetween(20, 300).WithMessage("Height must be between 20 and 300 cm.")
            .When(x => x.HeightCm.HasValue);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.")
            .When(x => x.Notes != null);
    }
}
