using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Departments
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Department code is required.")
                .MaximumLength(20).WithMessage("Code must not exceed 20 characters.")
                .Matches(@"^[A-Z0-9_]+$").WithMessage("Code must contain only uppercase letters, digits, or underscores.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.Location)
                .MaximumLength(100).WithMessage("Location must not exceed 100 characters.")
                .When(x => x.Location != null);

            RuleFor(x => x.HeadDoctorName)
                .MaximumLength(100).WithMessage("Head doctor name must not exceed 100 characters.")
                .When(x => x.HeadDoctorName != null);

            RuleFor(x => x.ContactExtension)
                .MaximumLength(20).WithMessage("Contact extension must not exceed 20 characters.")
                .Matches(@"^\d+$").WithMessage("Contact extension must contain only digits.")
                .When(x => x.ContactExtension != null);
        }
    }

}
