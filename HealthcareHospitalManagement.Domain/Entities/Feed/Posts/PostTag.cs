using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostTag : BaseEntity
    {
        // ——— ১. পোস্ট রেফারেন্স ———
        public      Guid                PostId                  { get; set; }

        public      Post                Post                    { get; set; } = null!;


        // ——— ২. ট্যাগ ডিটেইলস ———
        public      string              Tag                     { get; set; } = string.Empty;
    }
}