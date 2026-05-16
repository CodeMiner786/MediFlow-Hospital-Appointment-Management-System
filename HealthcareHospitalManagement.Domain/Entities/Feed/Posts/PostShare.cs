using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts
{
    public class PostShare : BaseEntity
    {
        // ——— ১. ইন্টারঅ্যাকশন রেফারেন্স ———
        public      Guid                PostInteractionId       { get; set; }

        public      PostInteraction     PostInteraction         { get; set; } = null!;


        // ——— ২. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ৩. শেয়ার সংক্রান্ত তথ্য ———
        public      string?             ShareNote               { get; set; }

        public      DateTime            SharedAt                { get; set; } = DateTime.UtcNow;
    }
}