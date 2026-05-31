using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorEarning
{
    public class MarkEarningPaidValidator : AbstractValidator<MarkEarningPaidDto>
    {
        public MarkEarningPaidValidator()
        {
            RuleFor(x => x.PaymentReference)
                .NotEmpty().WithMessage("Payment reference is required.")
                .MaximumLength(100).WithMessage("Payment reference must not exceed 100 characters.");
        }
    }

}
