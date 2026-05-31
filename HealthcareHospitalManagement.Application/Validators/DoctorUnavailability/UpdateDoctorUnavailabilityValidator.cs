using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorUnavailability
{
    public class UpdateDoctorUnavailabilityValidator : AbstractValidator<UpdateDoctorUnavailabilityDto>
    {
        public UpdateDoctorUnavailabilityValidator()
        {
            RuleFor(x => x.UnavailableDate)
                .NotEmpty().WithMessage("Unavailable date is required.");

            RuleFor(x => x.FromTime)
                .NotEmpty().WithMessage("From time is required when not a full-day unavailability.")
                .When(x => !x.IsFullDay);

            RuleFor(x => x.ToTime)
                .NotEmpty().WithMessage("To time is required when not a full-day unavailability.")
                .GreaterThan(x => x.FromTime!.Value).WithMessage("To time must be after from time.")
                .When(x => !x.IsFullDay && x.FromTime.HasValue);

            RuleFor(x => x.Reason)
                .MaximumLength(300).WithMessage("Reason must not exceed 300 characters.")
                .When(x => x.Reason != null);
        }
    }

}
