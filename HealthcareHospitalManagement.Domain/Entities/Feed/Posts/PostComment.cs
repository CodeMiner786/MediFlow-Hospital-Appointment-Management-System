using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostComment : BaseEntity
    {
        // ——— ১. ইন্টারঅ্যাকশন রেফারেন্স (আপডেট করা হয়েছে) ———
        // সরাসরি PostId এর বদলে এখন PostInteractionId ব্যবহার হবে
        public      Guid                PostInteractionId       { get; set; }

        public      PostInteraction     PostInteraction         { get; set; } = null!;


        // ——— ২. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ৩. কন্টেন্ট ডিটেইলস ———
        public      string              Content                 { get; set; } = string.Empty;


        // ——— ৪. সেলফ-রেফারেন্সিং (Nested Comments/Replies) ———
        public      Guid?               ParentCommentId         { get; set; }

        public      PostComment?        ParentComment           { get; set; }

        public      ICollection<PostComment>  Replies           { get; set; } = [];
    }
}