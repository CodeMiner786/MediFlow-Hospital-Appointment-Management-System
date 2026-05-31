using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorUnavailability
{
    public class CreateDoctorUnavailabilityValidator : AbstractValidator<CreateDoctorUnavailabilityDto>
    {
        public CreateDoctorUnavailabilityValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.UnavailableDate)
                .NotEmpty().WithMessage("Unavailable date is required.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Unavailable date must be today or in the future.");

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
