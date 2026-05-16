using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    /// <summary>
    /// Pharmacy department profile. Created by SuperAdmin.
    /// PharmacyAdmin manages medicines, orders, and creates listings.
    /// Payments go to Platform wallet.
    /// </summary>
    public class PharmacyProfile : BaseEntity
    {
        // ——— ১. ইউজার রিলেশন ———
        public      Guid                ApplicationUserId       { get; set; }
        public      ApplicationUser     ApplicationUser         { get; set; } = null!;

        // ——— ২. বেসিক ইনফরমেশন ———
        public      string              PharmacyName            { get; set; } = string.Empty;
        public      string              LicenseNumber           { get; set; } = string.Empty;
        public      string              ContactNumber           { get; set; } = string.Empty;
        public      string?             LogoUrl                 { get; set; }
        public      string              Address                 { get; set; } = string.Empty;
        public      string              City                    { get; set; } = string.Empty;

        // ——— ৩. স্ট্যাটাস ও সার্ভিস ———
        public      bool                IsVerified              { get; set; } = false;
        public      bool                IsActive                { get; set; } = true;
        public      bool                HasDeliveryService      { get; set; } = false;

        // ——— ৪. রেটিং ও ফিডব্যাক ———
        public      decimal             AverageRating           { get; set; } = 0;
        public      int                 TotalRatings            { get; set; } = 0;

        // ——— ৫. কালেকশনস ———
        public      ICollection<Medicine>               Medicines       { get; set; } = [];
        public      ICollection<PharmacyServiceListing> ServiceListings { get; set; } = [];
    }
}