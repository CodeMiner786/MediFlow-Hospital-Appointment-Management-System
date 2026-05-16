using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Feed
{
    public class FeedItemSaveRequestDto
    {
        public Guid FeedItemId { get; set; }
        public Guid UserId { get; set; }
    }

}
