using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorDocumentRepository : IGenericRepository<DoctorDocument>
    {
        IAsyncEnumerable<DoctorDocument> GetDocumentsByDoctorIdStream(Guid doctorId);
        IAsyncEnumerable<DoctorDocument> GetDocumentsByVerificationStatusStream(bool isVerified);
        IAsyncEnumerable<DoctorDocument> GetDocumentsByTypeStream(DoctorDocumentType type);
        IAsyncEnumerable<DoctorDocument> GetExpiringDocumentsStream(DateTime thresholdDate);

        // ✅ নতুন: Pagination সহ ডকুমেন্ট আনা
        Task<PagedResponse<DoctorDocument>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    }

}
