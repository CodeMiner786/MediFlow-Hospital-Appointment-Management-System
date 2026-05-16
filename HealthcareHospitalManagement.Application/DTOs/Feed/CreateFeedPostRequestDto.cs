using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;

namespace HealthcareHospitalManagement.Application.DTOs.Feed;

public class CreateFeedPostRequestDto
{
    public Guid                 OwnerUserId     { get; set; }
    public string               Content         { get; set; } = string.Empty;
    public PostType             Audience        { get; set; }
    public List<string>         MediaUrls       { get; set; } = [];
    public List<string>         Tags            { get; set; } = [];
}
