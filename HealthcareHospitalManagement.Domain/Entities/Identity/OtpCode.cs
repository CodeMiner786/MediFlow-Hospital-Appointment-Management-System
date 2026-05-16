using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.OTP;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    public class OtpCode : BaseEntity
    {
        // ——— ১. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ২. কোড ও পারপাস ———
        public      string              CodeHash                { get; set; } = string.Empty;

        public      OtpPurpose          Purpose                 { get; set; }

        public      string?             SentTo                  { get; set; } // Email or Phone


        // ——— ৩. ভ্যালিডেশন ও স্ট্যাটাস ———
        public      DateTime            ExpiresAt               { get; set; }

        public      bool                IsUsed                  { get; set; } = false;

        public      DateTime?           UsedAt                  { get; set; }


        // ——— ৪. সিকিউরিটি ও লিমিট ———
        public      int                 AttemptCount            { get; set; } = 0;

        public      int                 MaxAttempts             { get; set; } = 5;

        public      string?             IpAddress               { get; set; }


        // ——— ৫. লজিক্যাল প্রপার্টিজ ———
        public      bool                IsBlocked               => AttemptCount >= MaxAttempts;
        
        public      bool                IsExpired               => DateTime.UtcNow >= ExpiresAt;
        
        public      bool                IsValid                 => !IsUsed && !IsExpired && !IsBlocked;

        public string Code { get; set; } = string.Empty;
    }
}