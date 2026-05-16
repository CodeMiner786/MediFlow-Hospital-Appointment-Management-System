namespace HealthcareHospitalManagement.Application.DTOs.Feedback;

public class FeedbackSummaryResponseDto
{
    public Guid     TargetId        { get; set; }
    public string   TargetType      { get; set; } = string.Empty;
    public decimal  AverageRating   { get; set; }
    public int      TotalRatings    { get; set; }
    public int      FiveStar        { get; set; }
    public int      FourStar        { get; set; }
    public int      ThreeStar       { get; set; }
    public int      TwoStar         { get; set; }
    public int      OneStar         { get; set; }
}
