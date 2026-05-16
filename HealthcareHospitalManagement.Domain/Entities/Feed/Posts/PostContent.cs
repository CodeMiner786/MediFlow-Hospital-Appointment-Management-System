using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostContent : BaseEntity
    {
        // ——— ১. পোস্ট রেফারেন্স (One-to-One) ———
        public      Guid                PostId                  { get; set; }

        public      Post                Post                    { get; set; } = null!;


        // ——— ২. কন্টেন্ট ডিটেইলস ———
        public      string?             TextBody                { get; set; }

        public      PostType            PostType                { get; set; }


        // ——— ৩. মেটা ডেটা ও ফরম্যাটিং ———
        public      string?             Language                { get; set; }

        public      string?             FormattedContent        { get; set; }
    }
}