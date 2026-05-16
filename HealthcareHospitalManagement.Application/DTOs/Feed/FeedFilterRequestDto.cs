using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Application.DTOs.Feed;

public class FeedFilterRequestDto
{
    public FeedItemType?    ItemType    { get; set; }
    public string?          City        { get; set; }
    public string?          Tag         { get; set; }
    public int              PageNumber  { get; set; } = 1;
    public int              PageSize    { get; set; } = 10;
}
