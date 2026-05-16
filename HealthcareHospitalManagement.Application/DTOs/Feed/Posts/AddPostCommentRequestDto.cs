using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts;

public class AddPostCommentRequestDto
{
    public Guid     PostId      { get; set; }
    public Guid     UserId      { get; set; }
    public string   Content     { get; set; } = string.Empty;
    public Guid?    ParentCommentId { get; set; }
}
