using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts
{
    public class UpdatePostRequestDto
    {
        public Guid PostId { get; set; }
        public string? Content { get; set; }
        public PostVisibility? Audience { get; set; }
        public List<string>? Tags { get; set; }
        public List<string>? MediaUrls { get; set; }
        public bool? IsPinned { get; set; }
    }

}
