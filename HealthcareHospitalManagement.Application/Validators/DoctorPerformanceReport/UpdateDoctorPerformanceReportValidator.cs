using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorPerformanceReport
{
    public class UpdateDoctorPerformanceReportValidator : AbstractValidator<UpdateDoctorPerformanceReportDto>
    {
        public UpdateDoctorPerformanceReportValidator()
        {
            RuleFor(x => x.TotalAppointments)
                .GreaterThanOrEqualTo(0).WithMessage("Total appointments must be 0 or greater.");

            RuleFor(x => x.CompletedAppointments)
                .GreaterThanOrEqualTo(0).WithMessage("Completed appointments must be 0 or greater.")
                .LessThanOrEqualTo(x => x.TotalAppointments).WithMessage("Completed cannot exceed total appointments.");

            RuleFor(x => x.CancelledAppointments)
                .GreaterThanOrEqualTo(0).WithMessage("Cancelled appointments must be 0 or greater.");

            RuleFor(x => x.NoShows)
                .GreaterThanOrEqualTo(0).WithMessage("No-shows must be 0 or greater.");

            RuleFor(x => x.CompletionRate)
                .InclusiveBetween(0, 100).WithMessage("Completion rate must be between 0 and 100.");

            RuleFor(x => x.TotalPatientsSeen)
                .GreaterThanOrEqualTo(0).WithMessage("Total patients seen must be 0 or greater.");

            RuleFor(x => x.AverageConsultationMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("Average consultation minutes must be 0 or greater.");

            RuleFor(x => x.TotalRevenue)
                .GreaterThanOrEqualTo(0).WithMessage("Total revenue must be 0 or greater.");

            RuleFor(x => x.AverageRating)
                .InclusiveBetween(0, 5).WithMessage("Average rating must be between 0 and 5.");

            RuleFor(x => x.TotalReviews)
                .GreaterThanOrEqualTo(0).WithMessage("Total reviews must be 0 or greater.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid report status.");
        }
    }

}
