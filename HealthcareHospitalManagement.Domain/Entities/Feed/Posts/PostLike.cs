using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostLike : BaseEntity
    {
        // ——— ১. ইন্টারঅ্যাকশন রেফারেন্স (এরর দূর করার জন্য এটি জরুরি) ———
        public      Guid                PostInteractionId       { get; set; }

        public      PostInteraction     PostInteraction         { get; set; } = null!;


        // ——— ২. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;
    }
}