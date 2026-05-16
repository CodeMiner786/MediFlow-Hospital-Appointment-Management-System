using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts;

public class PostCommentResponseDto
{
    public Guid     Id              { get; set; }
    public string   AuthorName      { get; set; } = string.Empty;
    public string?  AuthorImageUrl  { get; set; }
    public string   Content         { get; set; } = string.Empty;
    public Guid?    ParentCommentId { get; set; }
    public int      ReplyCount      { get; set; }
    public DateTime CreatedAt       { get; set; }
}
