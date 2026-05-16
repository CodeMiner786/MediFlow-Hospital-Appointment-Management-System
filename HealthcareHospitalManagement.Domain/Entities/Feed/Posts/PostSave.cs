using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostSave : BaseEntity
    {
        // ——— ১. ইন্টারঅ্যাকশন রেফারেন্স ———
        public      Guid                PostInteractionId       { get; set; }

        public      PostInteraction     PostInteraction         { get; set; } = null!;


        // ——— ২. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ৩. সেভ ডিটেইলস ———
        public      DateTime            SavedAt                 { get; set; } = DateTime.UtcNow;
    }
}