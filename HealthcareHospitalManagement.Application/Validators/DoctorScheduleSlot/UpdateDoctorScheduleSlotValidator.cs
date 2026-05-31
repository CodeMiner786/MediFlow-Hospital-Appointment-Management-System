using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorScheduleSlot
{
    public class UpdateDoctorScheduleSlotValidator : AbstractValidator<UpdateDoctorScheduleSlotDto>
    {
        public UpdateDoctorScheduleSlotValidator()
        {
            RuleFor(x => x.SlotDate)
                .NotEmpty().WithMessage("Slot date is required.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("Slot start time is required.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("Slot end time is required.")
                .GreaterThan(x => x.SlotStartTime).WithMessage("Slot end time must be after start time.");

            RuleFor(x => x.BlockReason)
                .NotEmpty().WithMessage("Block reason is required when slot is blocked.")
                .MaximumLength(300).WithMessage("Block reason must not exceed 300 characters.")
                .When(x => x.IsBlocked);
        }
    }

}
