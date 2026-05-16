using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IPatientUnitOfWork : IDisposable
    {
        IPatientRepository Patients { get; }
        IHealthLogRepository HealthLogs { get; }
        IMedicalRecordRepository MedicalRecords { get; }
        IPatientReferralRepository PatientReferrals { get; }
        IPatientVitalRepository PatientVitals { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
