using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorAvailabilityLog
{
    public class UpdateDoctorAvailabilityLogValidator : AbstractValidator<UpdateDoctorAvailabilityLogDto>
    {
        public UpdateDoctorAvailabilityLogValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid availability status.");

            RuleFor(x => x.ChangedBy)
                .MaximumLength(100).WithMessage("ChangedBy must not exceed 100 characters.")
                .When(x => x.ChangedBy != null);

            RuleFor(x => x.Reason)
                .MaximumLength(300).WithMessage("Reason must not exceed 300 characters.")
                .When(x => x.Reason != null);

            RuleFor(x => x.ExpectedBackAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("ExpectedBackAt must be a future date/time.")
                .When(x => x.ExpectedBackAt.HasValue);
        }
    }

}
