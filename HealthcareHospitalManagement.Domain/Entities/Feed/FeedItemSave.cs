using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Feed
{
    public class FeedItemSave : BaseEntity
    {
        // ——— ১. ফিড আইটেম রেফারেন্স (Many-to-One) ———
        public      Guid                FeedItemId              { get; set; }

        public      FeedItem            FeedItem                { get; set; } = null!;


        // ——— ২. ইউজার রেফারেন্স (Many-to-One) ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;
    }
}