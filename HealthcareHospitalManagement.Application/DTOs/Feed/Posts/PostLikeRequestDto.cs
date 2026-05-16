using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Feed.Posts
{
    public class PostLikeRequestDto
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }

}
