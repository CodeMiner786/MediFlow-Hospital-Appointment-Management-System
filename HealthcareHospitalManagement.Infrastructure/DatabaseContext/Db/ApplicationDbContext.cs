using HealthcareHospitalManagement.Application.Interfaces;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Entities.Feedback;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using Microsoft.EntityFrameworkCore;
using System.Reflection;



namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;


public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // এটি আপনার Infrastructure প্রোজেক্টের সব Configuration ফাইল অটোমেটিক লোড করবে
        // ফলে আর ওই 'Id1' বা Shadow Property এরর আসবে না
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // ── Identity ────────────────────────────────────────────────────────────
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<OtpCode> OtpCodes { get; set; }
    public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
    public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }
    public DbSet<UserPermissionOverride> UserPermissionOverrides { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<RoleAssignmentLog> RoleAssignmentLogs { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    // ── Refresh Tokens ───────────────────────────────────────────────────────
    public DbSet<Domain.Entities.Identity.UserRefreshToken> UserRefreshTokens { get; set; }

    // ── Patient ──────────────────────────────────────────────────────────────
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientVital> PatientVitals { get; set; }
    public DbSet<HealthLog> HealthLogs { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }

    // ── Doctor ───────────────────────────────────────────────────────────────
    public DbSet<DoctorEntity> Doctors { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }
    public DbSet<StaffEntity> Staff { get; set; }
    public DbSet<StaffAttendance> StaffAttendances { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<DoctorScheduleSlot> DoctorScheduleSlots { get; set; }
    public DbSet<DoctorLeave> DoctorLeaves { get; set; }
    public DbSet<DoctorUnavailability> DoctorUnavailabilities { get; set; }
    public DbSet<DoctorAvailabilityLog> DoctorAvailabilityLogs { get; set; }
    public DbSet<DoctorNote> DoctorNotes { get; set; }
    public DbSet<DoctorEarning> DoctorEarnings { get; set; }
    public DbSet<DoctorDocument> DoctorDocuments { get; set; }
    public DbSet<DoctorFeedbackSummary> DoctorFeedbackSummaries { get; set; }
    public DbSet<DoctorPerformanceReport> DoctorPerformanceReports { get; set; }
    public DbSet<DoctorNotification> DoctorNotifications { get; set; }
    public DbSet<PatientReferral> PatientReferrals { get; set; }
    public DbSet<HospitalSettings> HospitalSettings { get; set; }

    // ── Appointment ──────────────────────────────────────────────────────────
    public DbSet<AppointmentEntity> Appointments { get; set; }
    public DbSet<AppointmentReminder> AppointmentReminders { get; set; }

    // ── Billing ──────────────────────────────────────────────────────────────
    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillItem> BillItems { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

    // ── Payment ──────────────────────────────────────────────────────────────
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<PlatformWallet> PlatformWallets { get; set; }
    public DbSet<PlatformWalletTransaction> PlatformWalletTransactions { get; set; }

    // ── Ambulance ────────────────────────────────────────────────────────────
    public DbSet<AmbulanceProviderProfile> AmbulanceProviderProfiles { get; set; }
    public DbSet<AmbulanceVehicle> AmbulanceVehicles { get; set; }
    public DbSet<AmbulanceBooking> AmbulanceBookings { get; set; }
    public DbSet<AmbulanceServiceListing> AmbulanceServiceListings { get; set; }
    public DbSet<AmbulanceProviderWallet> AmbulanceProviderWallets { get; set; }
    public DbSet<AmbulanceWalletTransaction> AmbulanceWalletTransactions { get; set; }

    // ── Lab ──────────────────────────────────────────────────────────────────
    public DbSet<LabProfile> LabProfiles { get; set; }
    public DbSet<LabTest> LabTests { get; set; }
    public DbSet<LabOrder> LabOrders { get; set; }
    public DbSet<LabOrderItem> LabOrderItems { get; set; }
    public DbSet<LabServiceListing> LabServiceListings { get; set; }

    // ── Pharmacy ─────────────────────────────────────────────────────────────
    public DbSet<PharmacyProfile> PharmacyProfiles { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<MedicineStock> MedicineStocks { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
    public DbSet<MedicineOrder> MedicineOrders { get; set; }
    public DbSet<MedicineOrderItem> MedicineOrderItems { get; set; }
    public DbSet<PharmacyServiceListing> PharmacyServiceListings { get; set; }

    // ── Ward ─────────────────────────────────────────────────────────────────
    public DbSet<Ward> Wards { get; set; }
    public DbSet<Bed> Beds { get; set; }
    public DbSet<Admission> Admissions { get; set; }
    public DbSet<BedBooking> BedBookings { get; set; }

    // ── Emergency ────────────────────────────────────────────────────────────
    public DbSet<EmergencyVisit> EmergencyVisits { get; set; }

    // ── Telemedicine ─────────────────────────────────────────────────────────
    public DbSet<TelemedicineSession> TelemedicineSessions { get; set; }

    // ── Chat ─────────────────────────────────────────────────────────────────
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    // ── Feed ─────────────────────────────────────────────────────────────────
    public DbSet<FeedItem> FeedItems { get; set; }
    public DbSet<FeedItemTag> FeedItemTags { get; set; }
    public DbSet<FeedItemLike> FeedItemLikes { get; set; }
    public DbSet<FeedItemSave> FeedItemSaves { get; set; }

    // ── Feedback ─────────────────────────────────────────────────────────────
    public DbSet<FeedbackEntity> Feedbacks { get; set; }

    // ── Notification ─────────────────────────────────────────────────────────
    public DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    // ── Analytics ────────────────────────────────────────────────────────────
    public DbSet<DashboardMetric> DashboardMetrics { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }

    // ── Dashboard ────────────────────────────────────────────────────────────
    public DbSet<AdminDashboardMetric> AdminDashboardMetrics { get; set; }
    public DbSet<DoctorDashboardMetric> DoctorDashboardMetrics { get; set; }
    public DbSet<AmbulanceDashboardMetric> AmbulanceDashboardMetrics { get; set; }
    public DbSet<LabDashboardMetric> LabDashboardMetrics { get; set; }
    public DbSet<PharmacyDashboardMetric> PharmacyDashboardMetrics { get; set; }
}
