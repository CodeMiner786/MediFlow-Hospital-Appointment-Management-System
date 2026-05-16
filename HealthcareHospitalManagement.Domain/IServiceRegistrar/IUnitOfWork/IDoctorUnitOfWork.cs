using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IDoctorUnitOfWork : IDisposable
    {
        IDoctorRepository Doctors { get; }
        IDepartmentRepository Departments { get; }
        IDoctorAvailabilityLogRepository DoctorAvailabilityLogs { get; }
        IDoctorDocumentRepository DoctorDocuments { get; }
        IDoctorEarningRepository DoctorEarnings { get; }
        IDoctorFeedbackSummaryRepository DoctorFeedbackSummaries { get; }
        IDoctorLeaveRepository DoctorLeaves { get; }
        IDoctorNoteRepository DoctorNotes { get; }
        IDoctorNotificationRepository DoctorNotifications { get; }
        IDoctorPerformanceReportRepository DoctorPerformanceReports { get; }
        IDoctorScheduleRepository DoctorSchedules { get; }
        IDoctorScheduleSlotRepository DoctorScheduleSlots { get; }
        IDoctorUnavailabilityRepository DoctorUnavailabilities { get; }
        IHospitalSettingsRepository HospitalSettings { get; }
        IStaffRepository Staff { get; }
        IStaffAttendanceRepository StaffAttendances { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
