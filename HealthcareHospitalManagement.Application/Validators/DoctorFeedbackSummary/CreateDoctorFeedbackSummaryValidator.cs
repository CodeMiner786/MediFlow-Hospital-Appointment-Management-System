using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorFeedbackSummary
{
    public class CreateDoctorFeedbackSummaryValidator : AbstractValidator<CreateDoctorFeedbackSummaryDto>
    {
        public CreateDoctorFeedbackSummaryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.AverageRating)
                .InclusiveBetween(0, 5).WithMessage("Average rating must be between 0 and 5.");

            RuleFor(x => x.TotalReviews)
                .GreaterThanOrEqualTo(0).WithMessage("Total reviews must be 0 or greater.");

            RuleFor(x => x.RatingCounts)
                .NotNull().WithMessage("Rating counts are required.");
        }
    }

}
