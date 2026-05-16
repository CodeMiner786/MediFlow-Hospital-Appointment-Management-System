using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts;

public class PostResponseDto
{
    public Guid     Id              { get; set; }
    public string   AuthorName      { get; set; } = string.Empty;
    public string?  AuthorImageUrl  { get; set; }
    public string   Content         { get; set; } = string.Empty;
    public string   Audience        { get; set; } = string.Empty;
    public int      LikeCount       { get; set; }
    public int      CommentCount    { get; set; }
    public int      ShareCount      { get; set; }
    public List<string> MediaUrls   { get; set; } = [];
    public List<string> Tags        { get; set; } = [];
    public DateTime CreatedAt       { get; set; }
}
