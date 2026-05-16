using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Enums.Feed.Posts
{
    public enum PostStatus
    {
        Draft = 1,
        Active = 2,
        Hidden = 3,
        Scheduled = 4,
        Deleted = 5,
        Published = 6
    }
}
