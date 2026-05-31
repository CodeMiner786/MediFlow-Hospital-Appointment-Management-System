using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.StaffAttendance
{
    public class CreateStaffAttendanceValidator : AbstractValidator<CreateStaffAttendanceDto>
    {
        public CreateStaffAttendanceValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage("Staff ID is required.");

            RuleFor(x => x.AttendanceDate)
                .NotEmpty().WithMessage("Attendance date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Attendance date cannot be in the future.");

            RuleFor(x => x.CheckOutTime)
                .GreaterThan(x => x.CheckInTime!.Value).WithMessage("Check-out time must be after check-in time.")
                .When(x => x.CheckInTime.HasValue && x.CheckOutTime.HasValue);

            RuleFor(x => x.LeaveReason)
                .NotEmpty().WithMessage("Leave reason is required when on leave.")
                .MaximumLength(300).WithMessage("Leave reason must not exceed 300 characters.")
                .When(x => x.IsOnLeave);

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
                .When(x => x.Notes != null);
        }
    }

}
