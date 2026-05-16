using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Emergency;


namespace HealthcareHospitalManagement.Application.Validators.Emergency;

public class UpdateEmergencyVisitRequestValidator : AbstractValidator<UpdateEmergencyVisitRequestDto>
{
    public UpdateEmergencyVisitRequestValidator()
    {
        RuleFor(x => x.EmergencyVisitId)
            .NotEmpty().WithMessage("EmergencyVisitId is required.")
            .NotEqual(Guid.Empty).WithMessage("EmergencyVisitId must not be an empty GUID.");

        RuleFor(x => x.Diagnosis)
            .MaximumLength(1000).WithMessage("Diagnosis must not exceed 1000 characters.")
            .When(x => x.Diagnosis != null);

        RuleFor(x => x.TreatmentGiven)
            .MaximumLength(2000).WithMessage("Treatment given must not exceed 2000 characters.")
            .When(x => x.TreatmentGiven != null);

        RuleFor(x => x.DischargeNotes)
            .MaximumLength(2000).WithMessage("Discharge notes must not exceed 2000 characters.")
            .When(x => x.DischargeNotes != null);

        RuleFor(x => x.DeathCause)
            .NotEmpty().WithMessage("Death cause is required when patient is deceased.")
            .When(x => x.IsDeceased);
    }
}
