using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorNotification
{
    public class UpdateDoctorNotificationValidator : AbstractValidator<UpdateDoctorNotificationDto>
    {
        public UpdateDoctorNotificationValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid notification category.");

            RuleFor(x => x.ActionUrl)
                .MaximumLength(500).WithMessage("Action URL must not exceed 500 characters.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Action URL must be a valid URL.")
                .When(x => x.ActionUrl != null);

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority type.");

            RuleFor(x => x.ExpiresAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("Expiry date must be in the future.")
                .When(x => x.ExpiresAt.HasValue);
        }
    }

}
