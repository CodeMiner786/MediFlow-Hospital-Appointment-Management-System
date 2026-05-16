using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.LoginWith;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    public class UserLoginHistory : BaseEntity
    {
        // ——— ১. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ২. লগইন স্ট্যাটাস ———
        public      DateTime            LoginAt                 { get; set; } = DateTime.UtcNow;

        public      bool                IsSuccess               { get; set; }

        public      string?             FailureReason           { get; set; }

        public      LoginProvider       LoginProvider           { get; set; } = LoginProvider.Local;


        // ——— ৩. ডিভাইস ও ব্রাউজার ডিটেইলস ———
        public      string?             DeviceName              { get; set; }

        public      string?             DeviceType              { get; set; }

        public      string?             Browser                 { get; set; }

        public      string?             OperatingSystem         { get; set; }

        public      string?             UserAgent               { get; set; }


        // ——— ৪. নেটওয়ার্ক ও লোকেশন ———
        public      string?             IpAddress               { get; set; }

        public      string?             Country                 { get; set; }

        public      string?             City                    { get; set; }
    }
}