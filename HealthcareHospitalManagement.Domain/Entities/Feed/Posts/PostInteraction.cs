using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostInteraction : BaseEntity
    {
        // ——— ১. পোস্ট রেফারেন্স (One-to-One) ———
        public      Guid                PostId                  { get; set; }

        public      Post                Post                    { get; set; } = null!;


        // ——— ২. ইন্টারেকশন কালেকশনস ———
        public      ICollection<PostLike>       Likes           { get; set; } = [];

        public      ICollection<PostComment>    Comments        { get; set; } = [];

        public      ICollection<PostShare>      Shares          { get; set; } = [];

        public      ICollection<PostSave>       Saves           { get; set; } = [];
    }
}