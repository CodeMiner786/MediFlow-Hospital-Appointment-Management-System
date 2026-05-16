using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Wards;


namespace HealthcareHospitalManagement.Application.Validators.Wards;

public class CreateAdmissionRequestValidator : AbstractValidator<CreateAdmissionRequestDto>
{
    public CreateAdmissionRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .NotEqual(Guid.Empty).WithMessage("PatientId must not be an empty GUID.");

        RuleFor(x => x.BedId)
            .NotEmpty().WithMessage("BedId is required.")
            .NotEqual(Guid.Empty).WithMessage("BedId must not be an empty GUID.");

        RuleFor(x => x.AdmittingDoctorId)
            .NotEmpty().WithMessage("AdmittingDoctorId is required.")
            .NotEqual(Guid.Empty).WithMessage("AdmittingDoctorId must not be an empty GUID.");

        RuleFor(x => x.AdmissionDate)
            .NotEmpty().WithMessage("Admission date is required.");

        RuleFor(x => x.AdmissionReason)
            .NotEmpty().WithMessage("Admission reason is required.")
            .Length(5, 1000).WithMessage("Admission reason must be between 5 and 1000 characters.");

        RuleFor(x => x.TransferredFrom)
            .NotEmpty().WithMessage("TransferredFrom is required when patient is transferred.")
            .When(x => x.IsTransferred);
    }
}
