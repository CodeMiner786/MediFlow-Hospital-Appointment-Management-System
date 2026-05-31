using HealthcareHospitalManagement.Domain.Enums.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary
{
    public class UpdateDoctorFeedbackSummaryDto
    {
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public Dictionary<RatingStar, int> RatingCounts { get; set; } = [];
    }

}
