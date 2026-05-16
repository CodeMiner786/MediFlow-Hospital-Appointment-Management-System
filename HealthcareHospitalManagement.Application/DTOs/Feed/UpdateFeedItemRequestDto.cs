using HealthcareHospitalManagement.Domain.Enums.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Feed
{
    public class UpdateFeedItemRequestDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? City { get; set; }
        public ServiceListingType? PrimaryAction { get; set; }
        public bool? IsAvailableNow { get; set; }
        public List<string>? Tags { get; set; }
    }

}
