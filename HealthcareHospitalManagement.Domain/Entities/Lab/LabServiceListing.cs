using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Domain.Entities.Lab
{
    /// <summary>
    /// LabAdmin can manually add custom service listings visible in the feed.
    /// E.g. "Complete Blood Count – 500 BDT", "Home Sample Collection"
    /// </summary>
    public class LabServiceListing : BaseEntity
    {
        // ——— ১. ল্যাব রেফারেন্স ———
        public      Guid                LabProfileId            { get; set; }

        public      LabProfile          LabProfile              { get; set; } = null!;


        // ——— ২. সার্ভিসের বিবরণ ———
        public      string              ServiceTitle            { get; set; } = string.Empty;

        public      string?             Description             { get; set; }


        // ——— ৩. প্রাইসিং ও লজিস্টিকস ———
        public      decimal             Price                   { get; set; }

        public      bool                HomeCollectionAvailable { get; set; } = false;

        public      decimal?            HomeCollectionCharge    { get; set; }


        // ——— ৪. স্ট্যাটাস ও ফিড কন্ট্রোল ———
        public      bool                IsActive                { get; set; } = true;

        public      FeedItemStatus      FeedStatus              { get; set; } = FeedItemStatus.Active;
    }
}