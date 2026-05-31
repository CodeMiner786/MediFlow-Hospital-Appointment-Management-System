using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorPerformanceReport
{
    public class CreateDoctorPerformanceReportValidator : AbstractValidator<CreateDoctorPerformanceReportDto>
    {
        public CreateDoctorPerformanceReportValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.FromDate)
                .NotEmpty().WithMessage("From date is required.");

            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("To date is required.")
                .GreaterThan(x => x.FromDate).WithMessage("To date must be after From date.");

            RuleFor(x => x.PeriodType)
                .IsInEnum().WithMessage("Invalid period type.");

            RuleFor(x => x.GeneratedBy)
                .MaximumLength(100).WithMessage("GeneratedBy must not exceed 100 characters.")
                .When(x => x.GeneratedBy != null);

            RuleFor(x => x.Department)
                .MaximumLength(100).WithMessage("Department must not exceed 100 characters.")
                .When(x => x.Department != null);
        }
    }

}
