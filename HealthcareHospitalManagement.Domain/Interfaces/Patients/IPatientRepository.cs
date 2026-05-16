using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Domain.Interfaces.Patients
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetByPatientCodeAsync(string patientCode, CancellationToken ct);
        Task<Patient?> GetByUserIdAsync(Guid userId, CancellationToken ct);
        Task<Patient?> GetByNationalIdAsync(string nid, CancellationToken ct);
        Task<Patient?> GetFullProfileByIdAsync(Guid patientId, CancellationToken ct);
        Task<IEnumerable<Patient>> GetPatientsByBloodGroupAsync(BloodGroup bloodGroup, CancellationToken ct);
        Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm, CancellationToken ct);
        Task<IEnumerable<Patient>> GetCurrentlyAdmittedPatientsAsync(CancellationToken ct);

        Task<Patient?> GetByApplicationUserIdAsync(Guid applicationUserId, CancellationToken ct);
    }
}