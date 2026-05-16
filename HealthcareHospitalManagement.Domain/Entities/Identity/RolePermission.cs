using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Permission;
using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    // Role-level module permissions (set by SuperAdmin)
    public class RolePermission : BaseEntity
    {
        // ——— ১. রোল এবং মডিউল ———
        public      UserRole            Role                    { get; set; }

        public      string              Module                  { get; set; } = string.Empty;


        // ——— ২. অ্যাকশন ও পারমিশন ———
        public      PermissionAction    Action                  { get; set; }

        public      bool                IsGranted               { get; set; } = true;


        // ——— ৩. মেটা ডাটা ———
        public      string?             Description             { get; set; }
    }
}