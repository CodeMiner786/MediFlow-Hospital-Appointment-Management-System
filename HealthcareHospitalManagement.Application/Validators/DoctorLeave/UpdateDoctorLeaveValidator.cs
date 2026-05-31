using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorLeave
{
    public class UpdateDoctorLeaveValidator : AbstractValidator<UpdateDoctorLeaveDto>
    {
        public UpdateDoctorLeaveValidator()
        {
            RuleFor(x => x.LeaveFrom)
                .NotEmpty().WithMessage("Leave start date is required.");

            RuleFor(x => x.LeaveTo)
                .NotEmpty().WithMessage("Leave end date is required.")
                .GreaterThanOrEqualTo(x => x.LeaveFrom).WithMessage("Leave end date must be on or after start date.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason is required.")
                .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.");

            RuleFor(x => x.LeaveType)
                .IsInEnum().WithMessage("Invalid leave type.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
                .When(x => x.Notes != null);
        }
    }


}
