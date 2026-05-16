using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostMedia : BaseEntity
    {
        // ——— ১. পোস্ট রেফারেন্স ———
        public      Guid                PostId                  { get; set; }

        public      Post                Post                    { get; set; } = null!;


        // ——— ২. মিডিয়া ডিটেইলস ———
        public      string              MediaUrl                { get; set; } = string.Empty;

        public      PostMediaType       MediaType               { get; set; }

        public      string?             ThumbnailUrl            { get; set; }


        // ——— ৩. ডিসপ্লে সেটিংস ———
        public      int                 SortOrder               { get; set; } = 0;
    }
}