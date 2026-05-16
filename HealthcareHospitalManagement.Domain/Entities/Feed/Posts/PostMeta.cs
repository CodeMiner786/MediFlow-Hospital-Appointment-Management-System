using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostMeta : BaseEntity
    {
        // ——— ১. পোস্ট রেফারেন্স (One-to-One) ———
        public      Guid                PostId                  { get; set; }

        public      Post                Post                    { get; set; } = null!;


        // ——— ২. স্ট্যাটাস এবং সেটিংস ———
        public      PostStatus          Status                  { get; set; } = PostStatus.Draft;

        public      bool                IsPinned                { get; set; } = false;

        public      DateTime?           PublishedAt             { get; set; }


        // ——— ৩. ট্যাগ কালেকশন ———
        public      ICollection<PostTag> Tags                   { get; set; } = [];
    }
}