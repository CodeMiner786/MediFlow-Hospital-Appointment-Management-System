using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts
{
    public class CreatePostRequestDto
    {
        public Guid OwnerUserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public PostType PostType { get; set; }
        public PostVisibility Audience { get; set; }
        public bool IsAnonymous { get; set; } = false;
        public string? TargetCity { get; set; }
        public string? TargetSpecialty { get; set; }
        public List<string> MediaUrls { get; set; } = [];
        public List<string> Tags { get; set; } = [];
        public string? Language { get; set; }
    }

}
