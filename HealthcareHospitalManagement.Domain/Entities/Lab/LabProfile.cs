using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Entities.Lab
{
    /// <summary>
    /// Lab department profile. Created by SuperAdmin.
    /// LabAdmin manages tests, results, and creates service listings.
    /// Lab payments go to Platform wallet.
    /// </summary>
    public class LabProfile : BaseEntity
    {
        // ——— ১. ইউজার ও আইডেন্টিটি ———
        public      Guid                ApplicationUserId       { get; set; }

        public      ApplicationUser     ApplicationUser         { get; set; } = null!;


        // ——— ২. ল্যাব ডিটেইলস ———
        public      string              LabName                 { get; set; } = string.Empty;

        public      string              RegistrationNumber      { get; set; } = string.Empty;

        public      string              ContactNumber           { get; set; } = string.Empty;

        public      string?             LogoUrl                 { get; set; }


        // ——— ৩. লোকেশন ও স্ট্যাটাস ———
        public      string              Address                 { get; set; } = string.Empty;

        public      string              City                    { get; set; } = string.Empty;

        public      bool                IsVerified              { get; set; } = false;

        public      bool                IsActive                { get; set; } = true;


        // ——— ৪. রেটিং ও পারফরম্যান্স ———
        public      decimal             AverageRating           { get; set; } = 0;

        public      int                 TotalRatings            { get; set; } = 0;


        // ——— ৫. কালেকশনস ———
        public      ICollection<LabTest>            Tests           { get; set; } = [];

        public      ICollection<LabServiceListing>  ServiceListings { get; set; } = [];
    }
}