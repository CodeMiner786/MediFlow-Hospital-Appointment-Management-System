using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorRepository : IGenericRepository<DoctorEntity>
    {
        Task<DoctorEntity?> GetByDoctorCodeAsync(string doctorCode);
        IAsyncEnumerable<DoctorEntity> GetDoctorsBySpecializationStream(DoctorSpecialization specialization);
        IAsyncEnumerable<DoctorEntity> GetDoctorsByDepartmentStream(Guid departmentId);
        Task<DoctorEntity?> GetDoctorFullProfileAsync(Guid doctorId);
        IAsyncEnumerable<DoctorEntity> GetDoctorsWithExpiringLicenseStream(DateTime thresholdDate);
        Task<IEnumerable<DoctorEntity>> GetTopRatedDoctorsAsync(int count);
       
    }
}