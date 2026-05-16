using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Permission;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IRolePermissionRepository : IGenericRepository<RolePermission>
    {
        // ১. একটি নির্দিষ্ট রোলের সব পারমিশন দেখা
        Task<IEnumerable<RolePermission>> GetPermissionsByRoleAsync(UserRole role);

        // ২. নির্দিষ্ট রোল এবং মডিউল অনুযায়ী পারমিশন চেক করা
        Task<bool> HasPermissionAsync(UserRole role, string module, PermissionAction action);

        // ৩. একটি রোলের সব পারমিশন ডিলিট করা (রোল রিসেট করার জন্য)
        Task RemovePermissionsByRoleAsync(UserRole role);

        // ৪. একটি নির্দিষ্ট মডিউলের আন্ডারে কোন কোন রোলের এক্সেস আছে তা দেখা
        Task<IEnumerable<RolePermission>> GetPermissionsByModuleAsync(string module);
    }
}
