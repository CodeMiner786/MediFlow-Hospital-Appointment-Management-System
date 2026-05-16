using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Application.DTOs.Feed;

public class CreateFeedItemRequestDto
{
    public Guid                 OwnerUserId     { get; set; }
    public Guid                 OwnerProfileId  { get; set; }
    public FeedItemType         ItemType        { get; set; }
    public string               Title           { get; set; } = string.Empty;
    public string?              SubTitle        { get; set; }
    public string?              Description     { get; set; }
    public string?              ImageUrl        { get; set; }
    public string?              CoverImageUrl   { get; set; }
    public double?              Latitude        { get; set; }
    public double?              Longitude       { get; set; }
    public string?              City            { get; set; }
    public ServiceListingType   PrimaryAction   { get; set; }
    public List<string>         Tags            { get; set; } = [];
}
