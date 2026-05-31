using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Doctors
{
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Today.AddYears(-22)).WithMessage("Doctor must be at least 22 years old.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender value.");

            RuleFor(x => x.Specialization)
                .IsInEnum().WithMessage("Invalid specialization value.");

            RuleFor(x => x.SubSpecialization)
                .IsInEnum().WithMessage("Invalid sub-specialization value.")
                .When(x => x.SubSpecialization.HasValue);

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

            RuleFor(x => x.TelemedicineConsultationFee)
                .GreaterThan(0).WithMessage("Telemedicine fee must be greater than 0.")
                .When(x => x.TelemedicineConsultationFee.HasValue);

            RuleFor(x => x.HomeVisitFee)
                .GreaterThan(0).WithMessage("Home visit fee must be greater than 0.")
                .When(x => x.HomeVisitFee.HasValue);

            RuleFor(x => x.CustomDoctorSharePercent)
                .InclusiveBetween(1, 100).WithMessage("Doctor share percent must be between 1 and 100.")
                .When(x => x.CustomDoctorSharePercent.HasValue);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }

}
