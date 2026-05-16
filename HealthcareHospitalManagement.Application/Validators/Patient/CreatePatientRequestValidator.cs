using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Patient;


namespace HealthcareHospitalManagement.Application.Validators.Patient;

public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequestDto>
{
    public CreatePatientRequestValidator()
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
            .LessThan(DateTime.Today).WithMessage("Date of birth cannot be in the future.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender.");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National ID is required.")
            .MaximumLength(30).WithMessage("National ID must not exceed 30 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Phone number must be a valid phone number.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(300).WithMessage("Address must not exceed 300 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .MaximumLength(100).WithMessage("State must not exceed 100 characters.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip code is required.")
            .MaximumLength(20).WithMessage("Zip code must not exceed 20 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.EmergencyContactName)
            .NotEmpty().WithMessage("Emergency contact name is required.")
            .Length(2, 100).WithMessage("Emergency contact name must be between 2 and 100 characters.");

        RuleFor(x => x.EmergencyContactPhone)
            .NotEmpty().WithMessage("Emergency contact phone is required.")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Emergency contact phone must be a valid phone number.");

        RuleFor(x => x.EmergencyContactRelation)
            .NotEmpty().WithMessage("Emergency contact relation is required.")
            .MaximumLength(50).WithMessage("Emergency contact relation must not exceed 50 characters.");

        RuleFor(x => x.InsuranceProvider)
            .NotEmpty().WithMessage("Insurance provider is required when patient has insurance.")
            .When(x => x.HasInsurance);

        RuleFor(x => x.InsurancePolicyNumber)
            .NotEmpty().WithMessage("Insurance policy number is required when patient has insurance.")
            .When(x => x.HasInsurance);
    }
}
