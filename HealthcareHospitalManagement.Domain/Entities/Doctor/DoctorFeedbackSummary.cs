using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Rating;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorFeedbackSummary : BaseEntity
{
    // ── ১. ডাক্তার রিলেশন ────────────────────────────────────────────────────────
    public    Guid                        DoctorId      { get; set; }
    public    DoctorEntity                Doctor        { get; set; } = null!;

    // ── ২. রেটিং স্ট্যাটাস ─────────────────────────────────────────────────────────
    public    decimal                     AverageRating { get; set; }
    public    int                         TotalReviews  { get; set; }
    
    // ৫-স্টার থেকে ১-স্টার পর্যন্ত কতটি করে রিভিউ আছে তার ডিকশনারি
    public    Dictionary<RatingStar, int> RatingCounts  { get; set; } = [];
    public    DateTime                    LastUpdatedAt { get; set; } = DateTime.UtcNow;
}