using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // ডিপার্টমেন্ট কোড (যেমন: CARD, NEURO) দিয়ে ডিপার্টমেন্ট খুঁজে বের করা
        Task<Department?> GetByCodeAsync(string code);

        // ডিপার্টমেন্টের নাম দিয়ে সার্চ করা
        Task<Department?> GetByNameAsync(string name);

        // কোনো নির্দিষ্ট লোকেশনে (যেমন: Floor 1) কয়টি ডিপার্টমেন্ট আছে তা দেখা
        IAsyncEnumerable<Department> GetDepartmentsByLocationStream(string location);

        // ডিপার্টমেন্টের সাথে তার সব ডাক্তারদের লিস্ট লোড করা
        Task<Department?> GetDepartmentWithDoctorsAsync(Guid departmentId);
    }
}
