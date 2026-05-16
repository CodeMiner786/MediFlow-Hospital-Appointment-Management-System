namespace HealthcareHospitalManagement.Application.DTOs.Feedback;

public class FeedbackResponseDto
{
    public Guid     Id              { get; set; }
    public string   ReviewerName    { get; set; } = string.Empty;
    public string?  ReviewerImage   { get; set; }
    public int      Rating          { get; set; }
    public string?  Comment         { get; set; }
    public string   TargetType      { get; set; } = string.Empty;
    public DateTime SubmittedAt     { get; set; }
}
