using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using System.Runtime.CompilerServices;

namespace HealthcareHospitalManagement.Domain.Interfaces.Emergency
{
    public interface IEmergencyVisitRepository : IGenericRepository<EmergencyVisit>
    {
        Task<EmergencyVisit?> GetByEmergencyCodeAsync(string code, CancellationToken ct);

        ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetActiveEmergencyVisitsStream(CancellationToken ct);

        ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetVisitsByTriageLevelStream(EmergencyLevel level, CancellationToken ct);

        ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetHistoryByPatientIdStream(Guid patientId, CancellationToken ct);

        ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetVisitsByDateRangeStream(DateTime start, DateTime end, CancellationToken ct);

        ConfiguredCancelableAsyncEnumerable<EmergencyVisit> GetVisitsByStatusStream(EmergencyStatus status, CancellationToken ct);

        Task<EmergencyVisit?> GetEmergencyVisitWithDetailsAsync(Guid id, CancellationToken ct);
    }
}
