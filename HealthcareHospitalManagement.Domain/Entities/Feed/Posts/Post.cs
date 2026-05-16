using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Feed.Posts;

public class Post : BaseEntity
{
    // ——— ১. অথরশিপ ও ওনারশিপ ———
    public      Guid                AuthorUserId            { get; set; }

    public      ApplicationUser     AuthorUser              { get; set; } = null!;


    // ——— ২. পোস্ট কন্টেন্ট ও সেটিংস (Owned Entities) ———
    public      PostContent         Content                 { get; set; } = null!;

    public      PostAudience        Audience                { get; set; } = null!;

    public      PostMeta            Meta                    { get; set; } = null!;


    // ——— ৩. মিডিয়া ও ইন্টারঅ্যাকশন ———
    public      ICollection<PostMedia>        MediaFiles    { get; set; } = [];

    public      PostInteraction             Interactions    { get; set; } = null!;


    // ——— ৪. ফিড আইটেম লিঙ্কিং (Optional) ———
    public      Guid?               LinkedFeedItemId        { get; set; }

    public      FeedItem?           LinkedFeedItem          { get; set; }
    public      ICollection<PostTag>        Tags            { get; set; } = new List<PostTag>();
}