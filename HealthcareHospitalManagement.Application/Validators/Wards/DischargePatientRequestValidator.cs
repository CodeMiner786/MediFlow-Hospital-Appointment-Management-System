using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Wards;


namespace HealthcareHospitalManagement.Application.Validators.Wards;

public class DischargePatientRequestValidator : AbstractValidator<DischargePatientRequestDto>
{
    public DischargePatientRequestValidator()
    {
        RuleFor(x => x.AdmissionId)
            .NotEmpty().WithMessage("AdmissionId is required.")
            .NotEqual(Guid.Empty).WithMessage("AdmissionId must not be an empty GUID.");

        RuleFor(x => x.DischargeDate)
            .NotEmpty().WithMessage("Discharge date is required.");

        RuleFor(x => x.DischargeNotes)
            .MaximumLength(2000).WithMessage("Discharge notes must not exceed 2000 characters.")
            .When(x => x.DischargeNotes != null);

        RuleFor(x => x.DischargeSummary)
            .MaximumLength(5000).WithMessage("Discharge summary must not exceed 5000 characters.")
            .When(x => x.DischargeSummary != null);

        RuleFor(x => x.TransferredTo)
            .NotEmpty().WithMessage("TransferredTo is required when patient is transferred.")
            .When(x => x.IsTransferred);

        RuleFor(x => x.TransferReason)
            .NotEmpty().WithMessage("Transfer reason is required when patient is transferred.")
            .When(x => x.IsTransferred);
    }
}
