using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Doctor;


namespace HealthcareHospitalManagement.Application.Validators.Doctor;

public class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequestDto>
{
    public UpdateDoctorRequestValidator()
    {
        RuleFor(x => x.Biography)
            .MaximumLength(2000).WithMessage("Biography must not exceed 2000 characters.")
            .When(x => x.Biography != null);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Phone number must be a valid phone number.")
            .When(x => x.PhoneNumber != null);

        RuleFor(x => x.ConsultationFee)
            .GreaterThan(0).WithMessage("Consultation fee must be greater than 0.")
            .When(x => x.ConsultationFee.HasValue);

        RuleFor(x => x.TelemedicineConsultationFee)
            .GreaterThan(0).WithMessage("Telemedicine fee must be greater than 0.")
            .When(x => x.TelemedicineConsultationFee.HasValue);

        RuleFor(x => x.HomeVisitFee)
            .GreaterThan(0).WithMessage("Home visit fee must be greater than 0.")
            .When(x => x.HomeVisitFee.HasValue);
    }
}
