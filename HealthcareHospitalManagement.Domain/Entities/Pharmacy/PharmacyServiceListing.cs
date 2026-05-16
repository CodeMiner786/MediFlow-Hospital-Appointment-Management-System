using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    /// <summary>
    /// PharmacyAdmin can manually add services/promotions visible in the feed.
    /// </summary>
    public class PharmacyServiceListing : BaseEntity
    {
        // ——— ১. ফার্মেসি প্রোফাইল রেফারেন্স ———
        public      Guid                PharmacyProfileId       { get; set; }
        public      PharmacyProfile     PharmacyProfile         { get; set; } = null!;


        // ——— ২. সার্ভিসের বিস্তারিত ———
        public      string              ServiceTitle            { get; set; } = string.Empty;

        public      string?             Description             { get; set; }

        public      decimal?            Price                   { get; set; }


        // ——— ৩. স্ট্যাটাস ও ভিজিবিলিটি ———
        public      bool                IsDeliveryAvailable     { get; set; } = false;

        public      bool                IsActive                { get; set; } = true;

        public      FeedItemStatus      FeedStatus              { get; set; } = FeedItemStatus.Active;

        public      string?             ImageUrl                { get; set; }
    }
}