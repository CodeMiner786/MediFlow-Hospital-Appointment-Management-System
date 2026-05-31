using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorScheduleSlot
{
    public class CreateDoctorScheduleSlotValidator : AbstractValidator<CreateDoctorScheduleSlotDto>
    {
        public CreateDoctorScheduleSlotValidator()
        {
            RuleFor(x => x.DoctorScheduleId)
                .NotEmpty().WithMessage("Doctor Schedule ID is required.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.SlotDate)
                .NotEmpty().WithMessage("Slot date is required.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Slot date must be today or in the future.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("Slot start time is required.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("Slot end time is required.")
                .GreaterThan(x => x.SlotStartTime).WithMessage("Slot end time must be after start time.");
        }
    }

}
