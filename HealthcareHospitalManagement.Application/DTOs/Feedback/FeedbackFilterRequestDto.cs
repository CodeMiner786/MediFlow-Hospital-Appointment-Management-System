namespace HealthcareHospitalManagement.Application.DTOs.Feedback;

public class FeedbackFilterRequestDto
{
    public Guid?    TargetId    { get; set; }
    public string?  TargetType  { get; set; }
    public int?     MinRating   { get; set; }
    public int?     MaxRating   { get; set; }
    public int      PageNumber  { get; set; } = 1;
    public int      PageSize    { get; set; } = 10;
}
