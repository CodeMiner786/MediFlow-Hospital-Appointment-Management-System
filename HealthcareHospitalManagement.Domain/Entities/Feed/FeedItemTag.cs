using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Feed
{
    public class FeedItemTag : BaseEntity
    {
        // ——— ১. ফিড আইটেম রেফারেন্স (Many-to-One) ———
        public      Guid                FeedItemId              { get; set; }

        public      FeedItem            FeedItem                { get; set; } = null!;


        // ——— ২. ট্যাগ ডিটেইলস ———
        public      string              Tag                     { get; set; } = string.Empty;
    }
}