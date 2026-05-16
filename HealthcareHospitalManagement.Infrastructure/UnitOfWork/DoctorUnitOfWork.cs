using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Doctor;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class DoctorUnitOfWork(ApplicationDbContext context) : IDoctorUnitOfWork
    {
        public IDoctorRepository Doctors { get; } = new DoctorRepository(context);
        public IDepartmentRepository Departments { get; } = new DepartmentRepository(context);
        public IDoctorAvailabilityLogRepository DoctorAvailabilityLogs { get; } = new DoctorAvailabilityLogRepository(context);
        public IDoctorDocumentRepository DoctorDocuments { get; } = new DoctorDocumentRepository(context);
        public IDoctorEarningRepository DoctorEarnings { get; } = new DoctorEarningRepository(context);
        public IDoctorFeedbackSummaryRepository DoctorFeedbackSummaries { get; } = new DoctorFeedbackSummaryRepository(context);
        public IDoctorLeaveRepository DoctorLeaves { get; } = new DoctorLeaveRepository(context);
        public IDoctorNoteRepository DoctorNotes { get; } = new DoctorNoteRepository(context);
        public IDoctorNotificationRepository DoctorNotifications { get; } = new DoctorNotificationRepository(context);
        public IDoctorPerformanceReportRepository DoctorPerformanceReports { get; } = new DoctorPerformanceReportRepository(context);
        public IDoctorScheduleRepository DoctorSchedules { get; } = new DoctorScheduleRepository(context);
        public IDoctorScheduleSlotRepository DoctorScheduleSlots { get; } = new DoctorScheduleSlotRepository(context);
        public IDoctorUnavailabilityRepository DoctorUnavailabilities { get; } = new DoctorUnavailabilityRepository(context);
        public IHospitalSettingsRepository HospitalSettings { get; } = new HospitalSettingsRepository(context);
        public IStaffRepository Staff { get; } = new StaffRepository(context);
        public IStaffAttendanceRepository StaffAttendances { get; } = new StaffAttendanceRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}
