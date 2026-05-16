using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Patients;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class PatientUnitOfWork(ApplicationDbContext context) : IPatientUnitOfWork
    {
        public IPatientRepository Patients { get; } = new PatientRepository(context);
        public IHealthLogRepository HealthLogs { get; } = new HealthLogRepository(context);
        public IMedicalRecordRepository MedicalRecords { get; } = new MedicalRecordRepository(context);
        public IPatientReferralRepository PatientReferrals { get; } = new PatientReferralRepository(context);
        public IPatientVitalRepository PatientVitals { get; } = new PatientVitalRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
