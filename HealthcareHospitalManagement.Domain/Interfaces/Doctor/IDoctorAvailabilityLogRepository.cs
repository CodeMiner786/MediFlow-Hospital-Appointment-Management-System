using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorAvailabilityLogRepository : IGenericRepository<DoctorAvailabilityLog>
    {
        IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByDoctorIdStream(Guid doctorId);
        IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByStatusStream(DoctorAvailabilityStatus status);
        IAsyncEnumerable<DoctorAvailabilityLog> GetLogsByDateRangeStream(DateTime start, DateTime end);
        Task<DoctorAvailabilityLog?> GetLatestLogByDoctorIdAsync(Guid doctorId);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Pagination সহ লগ আনা
        Task<PagedResponse<DoctorAvailabilityLog>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    }

}
