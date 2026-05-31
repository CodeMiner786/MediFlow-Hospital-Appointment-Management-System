using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Staff
{
    public class CreateStaffValidator : AbstractValidator<CreateStaffDto>
    {
        public CreateStaffValidator()
        {
            RuleFor(x => x.ApplicationUserId)
                .NotEmpty().WithMessage("Application user ID is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Today.AddYears(-18)).WithMessage("Staff must be at least 18 years old.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender value.");

            RuleFor(x => x.StaffCode)
                .NotEmpty().WithMessage("Staff code is required.")
                .MaximumLength(20).WithMessage("Staff code must not exceed 20 characters.");

            RuleFor(x => x.StaffType)
                .IsInEnum().WithMessage("Invalid staff type.");

            RuleFor(x => x.Designation)
                .NotEmpty().WithMessage("Designation is required.")
                .MaximumLength(100).WithMessage("Designation must not exceed 100 characters.");

            RuleFor(x => x.Qualifications)
                .NotEmpty().WithMessage("Qualifications are required.")
                .MaximumLength(500).WithMessage("Qualifications must not exceed 500 characters.");

            RuleFor(x => x.JoiningDate)
                .NotEmpty().WithMessage("Joining date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Joining date cannot be in the future.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.ShiftType)
                .IsInEnum().WithMessage("Invalid shift type.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(300).WithMessage("Address must not exceed 300 characters.");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("National ID is required.")
                .MaximumLength(30).WithMessage("National ID must not exceed 30 characters.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }

}
