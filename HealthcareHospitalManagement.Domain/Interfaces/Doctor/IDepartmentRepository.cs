using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDepartmentRepository : IGenericRepository<DepartmentEntity>
    {
        // ডিপার্টমেন্ট কোড (যেমন: CARD, NEURO) দিয়ে ডিপার্টমেন্ট খুঁজে বের করা
        Task<DepartmentEntity?> GetByCodeAsync(string code);

        // ডিপার্টমেন্টের নাম দিয়ে সার্চ করা
        Task<DepartmentEntity?> GetByNameAsync(string name);

        // কোনো নির্দিষ্ট লোকেশনে (যেমন: Floor 1) কয়টি ডিপার্টমেন্ট আছে তা দেখা
        IAsyncEnumerable<DepartmentEntity> GetDepartmentsByLocationStream(string location);

        // ডিপার্টমেন্টের সাথে তার সব ডাক্তারদের লিস্ট লোড করা
        Task<DepartmentEntity?> GetDepartmentWithDoctorsAsync(Guid departmentId);

        // ✅ নতুন: Pagination সহ ডিপার্টমেন্ট লিস্ট আনা
        Task<PagedResponse<DepartmentEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    }

}
