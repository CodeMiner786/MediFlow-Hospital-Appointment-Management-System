using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IUserPermissionOverrideRepository : IGenericRepository<UserPermissionOverride>
    {
        // ১. নির্দিষ্ট একজন ইউজারের সব ওভাররাইড পারমিশন দেখা
        Task<IEnumerable<UserPermissionOverride>> GetOverridesByUserIdAsync(Guid userId);

        // ২. চেক করা: এই ইউজারের কি স্পেসিফিক কোনো মডিউলে ওভাররাইড রুল আছে?
        Task<UserPermissionOverride?> GetOverrideAsync(Guid userId, string module, PermissionAction action);

        // ৩. ইউজারের সব ওভাররাইড রুল ডিলিট করা (Reset to Role default)
        Task RemoveAllOverridesAsync(Guid userId);

        // ৪. নির্দিষ্ট মডিউলে কতজন ইউজারকে এক্সেপশনাল পারমিশন দেওয়া হয়েছে
        Task<IEnumerable<UserPermissionOverride>> GetUsersByModuleOverrideAsync(string module);
    }
}
