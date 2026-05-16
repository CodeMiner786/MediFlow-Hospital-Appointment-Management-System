using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts;

public class PostAudience : BaseEntity
{
    // ——— ১. পোস্ট রেফারেন্স (One-to-One) ———
    public      Guid                PostId                  { get; set; }

    public      Post                Post                    { get; set; } = null!;


    // ——— ২. ভিজিবিলিটি ও টার্গেটিং ———
    public      PostVisibility      Visibility              { get; set; } = PostVisibility.Public;

    public      string?             TargetCity              { get; set; }

    public      string?             TargetSpecialty         { get; set; }


    // ——— ৩. সেটিংস ———
    public      bool                IsAnonymous             { get; set; } = false;
}