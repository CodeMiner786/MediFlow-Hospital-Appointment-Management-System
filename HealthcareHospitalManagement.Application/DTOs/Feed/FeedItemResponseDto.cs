using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Application.DTOs.Feed;

public class FeedItemResponseDto
{
    public Guid     Id              { get; set; }
    public string   OwnerName       { get; set; } = string.Empty;
    public string?  OwnerImageUrl   { get; set; }
    public string   ItemType        { get; set; } = string.Empty;
    public string   Title           { get; set; } = string.Empty;
    public string?  SubTitle        { get; set; }
    public string?  Description     { get; set; }
    public string?  ImageUrl        { get; set; }
    public string?  City            { get; set; }
    public decimal  AverageRating   { get; set; }
    public int      TotalRatings    { get; set; }
    public bool     IsAvailableNow  { get; set; }
    public string   PrimaryAction   { get; set; } = string.Empty;
    public int      LikeCount       { get; set; }
    public int      SaveCount       { get; set; }
    public List<string> Tags        { get; set; } = [];
    public DateTime CreatedAt       { get; set; }
}
