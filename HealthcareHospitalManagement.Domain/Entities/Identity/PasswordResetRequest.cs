using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    public class PasswordResetRequest : BaseEntity
    {
        // ——— ১. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ২. টোকেন ও সিকিউরিটি ———
        public      string              TokenHash               { get; set; } = string.Empty;

        public      string?             IpAddress               { get; set; }

        public      string?             UserAgent               { get; set; }


        // ——— ৩. ভ্যালিডেশন ও স্ট্যাটাস ———
        public      DateTime            ExpiresAt               { get; set; }

        public      bool                IsUsed                  { get; set; } = false;

        public      DateTime?           UsedAt                  { get; set; }


        // ——— ৪. লজিক্যাল প্রপার্টিজ ———
        public      bool                IsExpired               => DateTime.UtcNow >= ExpiresAt;
        
        public      bool                IsValid                 => !IsUsed && !IsExpired;
    }
}