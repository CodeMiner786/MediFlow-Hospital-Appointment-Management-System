using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorLeave
{
    public class ApproveDoctorLeaveValidator : AbstractValidator<ApproveDoctorLeaveDto>
    {
        public ApproveDoctorLeaveValidator()
        {
            RuleFor(x => x.ApprovedBy)
                .NotEmpty().WithMessage("ApprovedBy is required.")
                .MaximumLength(100).WithMessage("ApprovedBy must not exceed 100 characters.");

            RuleFor(x => x.RejectionReason)
                .NotEmpty().WithMessage("Rejection reason is required when rejecting.")
                .MaximumLength(500).WithMessage("Rejection reason must not exceed 500 characters.")
                .When(x => !x.IsApproved);
        }
    }

}
