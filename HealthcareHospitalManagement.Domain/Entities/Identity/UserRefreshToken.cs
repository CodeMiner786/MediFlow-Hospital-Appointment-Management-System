using HealthcareHospitalManagement.Domain.Common.BaseModel;
using System;

namespace HealthcareHospitalManagement.Domain.Entities.Identity;
    public class UserRefreshToken : BaseEntity
    {
        // ——— ১. ইউজার রেফারেন্স ———
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        // ——— ২. টোকেন ———
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }

        // ✅ নতুন property যোগ করো
        public string JwtId { get; set; } = string.Empty;

        // ——— ৩. স্ট্যাটাস ———
        public bool IsRevoked { get; set; } = false;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        // ——— ৪. লজিক্যাল প্রপার্টিজ ———
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => !IsRevoked && !IsExpired;
    }

