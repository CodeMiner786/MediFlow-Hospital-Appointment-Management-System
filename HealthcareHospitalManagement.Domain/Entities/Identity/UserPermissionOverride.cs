using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Permission;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    public class UserPermissionOverride : BaseEntity
    {
        // ——— ১. ইউজার রেফারেন্স ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ২. পারমিশন ডিটেইলস ———
        public      string              Module                  { get; set; } = string.Empty;

        public      PermissionAction    Action                  { get; set; } // Create, Read, Update, Delete...

        public      bool                IsGranted               { get; set; } = true;


        // ——— ৩. অডিট ট্রেইল ———
        public      string?             GrantedBy               { get; set; }

        public      string?             Reason                  { get; set; }

        public      DateTime            GrantedAt               { get; set; } = DateTime.UtcNow;

        public      string?             IpAddress               { get; set; }

        public      string?             UserAgent               { get; set; }
    }
}