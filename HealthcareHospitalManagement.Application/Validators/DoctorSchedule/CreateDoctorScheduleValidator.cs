using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorSchedule
{
    public class CreateDoctorScheduleValidator : AbstractValidator<CreateDoctorScheduleDto>
    {
        public CreateDoctorScheduleValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.DayOfWeek)
                .IsInEnum().WithMessage("Invalid day of week.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");

            RuleFor(x => x.ShiftType)
                .IsInEnum().WithMessage("Invalid shift type.");

            RuleFor(x => x.MaxAppointments)
                .GreaterThan(0).WithMessage("Max appointments must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Max appointments must not exceed 100.");

            RuleFor(x => x.SlotDurationMinutes)
                .InclusiveBetween(5, 120).WithMessage("Slot duration must be between 5 and 120 minutes.");

            RuleFor(x => x.Location)
                .MaximumLength(100).WithMessage("Location must not exceed 100 characters.")
                .When(x => x.Location != null);

            RuleFor(x => x.SetBy)
                .NotEmpty().WithMessage("SetBy is required.")
                .MaximumLength(100).WithMessage("SetBy must not exceed 100 characters.");

            RuleFor(x => x.EffectiveFrom)
                .NotEmpty().WithMessage("Effective from date is required.");

            RuleFor(x => x.EffectiveTo)
                .GreaterThan(x => x.EffectiveFrom).WithMessage("Effective to date must be after effective from date.")
                .When(x => x.EffectiveTo.HasValue);
        }
    }

}
