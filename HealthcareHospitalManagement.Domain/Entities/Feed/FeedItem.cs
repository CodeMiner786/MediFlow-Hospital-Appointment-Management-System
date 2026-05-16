using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Domain.Entities.Feed
{
    public class FeedItem : BaseEntity
    {
        // ——— ১. বেসিক টাইপ ও স্ট্যাটাস ———
        public      FeedItemType        ItemType                { get; set; }

        public      FeedItemStatus      Status                  { get; set; } = FeedItemStatus.Active;


        // ——— ২. ওনারশিপ ও প্রোফাইল (Required) ———
        public      Guid                OwnerProfileId          { get; set; }

        public      Guid                OwnerUserId             { get; set; }

        public      ApplicationUser     OwnerUser               { get; set; } = null!;


        // ——— ৩. কন্টেন্ট ও মিডিয়া ———
        public      string              Title                   { get; set; } = string.Empty;

        public      string?             SubTitle                { get; set; }

        public      string?             Description             { get; set; }

        public      string?             ImageUrl                { get; set; }

        public      string?             CoverImageUrl           { get; set; }


        // ——— ৪. লোকেশন ডেটা ———
        public      double?             Latitude                { get; set; }

        public      double?             Longitude               { get; set; }

        public      string?             City                    { get; set; }


        // ——— ৫. রেটিং ও পারফরম্যান্স ———
        public      decimal             AverageRating           { get; set; } = 0;

        public      int                 TotalRatings            { get; set; } = 0;


        // ——— ৬. অ্যাকশন ও অ্যাভেইলেবিলিটি ———
        public      ServiceListingType  PrimaryAction           { get; set; }

        public      bool                IsAvailableNow          { get; set; } = true;


        // ——— ৭. কালেকশন (Relationships) ———
        public      ICollection<FeedItemTag>  Tags              { get; set; } = [];

        public      ICollection<FeedItemLike> Likes             { get; set; } = [];

        public      ICollection<FeedItemSave> Saves             { get; set; } = [];

        public      ICollection<Post>         LinkedPosts       { get; set; } = [];
    }
}