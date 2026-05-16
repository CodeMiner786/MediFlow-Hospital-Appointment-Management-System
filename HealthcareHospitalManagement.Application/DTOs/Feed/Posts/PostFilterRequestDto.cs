using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts
{
    public class PostFilterRequestDto
    {
        public PostType? PostType { get; set; }
        public PostVisibility? Visibility { get; set; }
        public PostStatus? Status { get; set; }
        public string? Tag { get; set; }
        public string? Keyword { get; set; }
        public Guid? AuthorId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
