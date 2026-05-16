using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Doctor;

namespace HealthcareHospitalManagement.Application.Validators.Doctor;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequestDto>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(x => x.ApplicationUserId)
            .NotEmpty().WithMessage("ApplicationUserId is required.")
            .NotEqual(Guid.Empty).WithMessage("ApplicationUserId must not be an empty GUID.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Today.AddYears(-18)).WithMessage("Doctor must be at least 18 years old.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender.");

        RuleFor(x => x.Specialization)
            .IsInEnum().WithMessage("Invalid specialization.");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("License number is required.")
            .MaximumLength(50).WithMessage("License number must not exceed 50 characters.");

        RuleFor(x => x.LicenseExpiryDate)
            .NotEmpty().WithMessage("License expiry date is required.")
            .GreaterThan(DateTime.Today).WithMessage("License must not be expired.");

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Experience years must be 0 or greater.")
            .LessThanOrEqualTo(60).WithMessage("Experience years must not exceed 60.");

        RuleFor(x => x.Qualifications)
            .NotEmpty().WithMessage("Qualifications are required.")
            .MaximumLength(500).WithMessage("Qualifications must not exceed 500 characters.");

        RuleFor(x => x.ConsultationFee)
            .GreaterThan(0).WithMessage("Consultation fee must be greater than 0.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Phone number must be a valid phone number.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("DepartmentId is required.")
            .NotEqual(Guid.Empty).WithMessage("DepartmentId must not be an empty GUID.");

        RuleFor(x => x.Biography)
            .MaximumLength(2000).WithMessage("Biography must not exceed 2000 characters.")
            .When(x => x.Biography != null);

        RuleFor(x => x.IsAvailableNow)
            .Must(v => v == true || v == false)
            .WithMessage("IsAvailableNow must be either true or false.");


    }
}
