using HealthcareHospitalManagement.Domain.Enums.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary
{
    public class DoctorFeedbackSummaryDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public Dictionary<RatingStar, int> RatingCounts { get; set; } = [];
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
