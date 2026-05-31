using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorEarning
{
    public class CreateDoctorEarningValidator : AbstractValidator<CreateDoctorEarningDto>
    {
        public CreateDoctorEarningValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.EarningDate)
                .NotEmpty().WithMessage("Earning date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Earning date cannot be in the future.");

            RuleFor(x => x.TotalFee)
                .GreaterThan(0).WithMessage("Total fee must be greater than 0.");

            RuleFor(x => x.HospitalSharePercent)
                .InclusiveBetween(1, 100).WithMessage("Hospital share percent must be between 1 and 100.");

            RuleFor(x => x.EarningType)
                .IsInEnum().WithMessage("Invalid earning type.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
                .When(x => x.Notes != null);
        }
    }

}
